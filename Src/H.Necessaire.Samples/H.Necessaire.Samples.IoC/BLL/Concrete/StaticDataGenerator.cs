using System.Threading.Tasks;

namespace H.Necessaire.Samples.IoC.BLL.Concrete
{
    [ID("static")]
    [Alias("st")]
    internal class StaticDataGenerator : ImADataGenerator
    {
        public Task<int[]> GenerateIntegers()
        {
            return 0.AsArray().AsTask();
        }
    }
}
