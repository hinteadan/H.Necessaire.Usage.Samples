using H.Necessaire.CLI;
using H.Necessaire.Runtime.CLI;
using System.Threading.Tasks;

namespace H.Necessaire.Samples.Runtime.CLI.Basic
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await new CliApp()
                .WithEverything()
                .Run()
                ;
        }
    }
}
