using System.Threading.Tasks;

namespace H.Necessaire.Samples.Caching
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ImADependencyProvider depsProvider
                = IoC.NewDependencyRegistry()
                .Register<HNecessaireDependencyGroup>(() => new HNecessaireDependencyGroup())
                .Register<ImACacher<SampleData>>(() => new CustomCacher<SampleData>())
                .Register<StringCacher>(() => new StringCacher())
                ;

            ImACacher<SampleData> cacher = depsProvider.GetCacher<SampleData>();

            SampleData data = await cacher.GetOrAdd("SomeData", id => new SampleData { Name = "My Sample Data" }.ToCacheableItem(id).AsTask());

            SampleData dataFromCache = (await cacher.TryGet("SomeData")).Payload;


            ImACacher<string> stringCacher = depsProvider.GetCacher<string>("string");

            string someString = await stringCacher.GetOrAdd("SomeString", id => "My Sample String".ToCacheableItem(id).AsTask());

            string someStringFromCache = (await stringCacher.TryGet("SomeString")).Payload;
        }
    }
}
