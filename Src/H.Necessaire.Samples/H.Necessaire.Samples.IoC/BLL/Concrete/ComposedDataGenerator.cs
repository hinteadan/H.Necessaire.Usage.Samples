using System.Linq;
using System.Threading.Tasks;

namespace H.Necessaire.Samples.IoC.BLL.Concrete
{
    [ID("composed")]
    internal class ComposedDataGenerator : ImADataGenerator, ImADependency
    {
        ImADataGenerator staticDataGenerator;
        ImADataGenerator randomDataGenerator;
        public void ReferDependencies(ImADependencyProvider dependencyProvider)
        {
            staticDataGenerator = dependencyProvider.Build<ImADataGenerator>("static");
            randomDataGenerator = dependencyProvider.Build<ImADataGenerator>("random");
        }

        public async Task<int[]> GenerateIntegers()
        {
            int[][] dataParts = await Task.WhenAll(
                staticDataGenerator.GenerateIntegers(),
                randomDataGenerator.GenerateIntegers()
            );

            return dataParts.SelectMany(part => part).ToArray();
        }
    }
}
