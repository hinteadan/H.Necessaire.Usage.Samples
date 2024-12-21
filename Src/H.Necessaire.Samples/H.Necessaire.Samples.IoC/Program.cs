using H.Necessaire.Samples.IoC.BLL;
using System;
using System.Threading.Tasks;

namespace H.Necessaire.Samples.IoC
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ImADependencyRegistry depsRegistry = H.Necessaire.IoC.NewDependencyRegistry();

            depsRegistry.Register<BLL.DependencyGroup>(() => new BLL.DependencyGroup());

            ImADependencyProvider depsProvider = depsRegistry;

            ImADataGenerator staticDataGenerator = depsProvider.Build<ImADataGenerator>("static");
            ImADataGenerator randomDataGenerator = depsProvider.Build<ImADataGenerator>("random");
            ImADataGenerator composedDataGenerator = depsProvider.Build<ImADataGenerator>("composed");

            Console.WriteLine(string.Join(", ", await staticDataGenerator.GenerateIntegers()));
            Console.WriteLine(string.Join(", ", await randomDataGenerator.GenerateIntegers()));
            Console.WriteLine(string.Join(", ", await composedDataGenerator.GenerateIntegers()));
        }
    }
}
