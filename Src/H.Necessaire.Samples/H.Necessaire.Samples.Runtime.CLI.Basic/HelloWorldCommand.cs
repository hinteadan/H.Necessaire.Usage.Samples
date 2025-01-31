using H.Necessaire.CLI.Commands;
using System;
using System.Threading.Tasks;

namespace H.Necessaire.Samples.Runtime.CLI.Basic
{
    internal class HelloWorldCommand : CommandBase
    {
        public override Task<OperationResult> Run()
        {
            Console.WriteLine("Hello from the lovely H.Necessaire CLI 💖!");

            return OperationResult.Win().AsTask();
        }
    }
}
