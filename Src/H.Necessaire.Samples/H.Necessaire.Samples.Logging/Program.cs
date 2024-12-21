using System.Threading.Tasks;

namespace H.Necessaire.Samples.Logging
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ImADependencyProvider depsProvider
                = H.Necessaire.IoC
                .NewDependencyRegistry()
                .Register<HNecessaireDependencyGroup>(() => new HNecessaireDependencyGroup())
                ;

            ImALogger logger = depsProvider.GetLogger<Program>();

            await logger.LogInfo("Hello H.Necessaire logging");
        }
    }
}
