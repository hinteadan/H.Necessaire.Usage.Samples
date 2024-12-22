using System;
using System.Threading.Tasks;

namespace H.Necessaire.Samples.Resilience
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            OperationResult resultA = await DoSomeWorkAlignedWithResilientPattern(logger: null);
            OperationResult resultB = await WrapSomeWorkWitResilientPattern(logger: null);

            OperationResult[] allResults = [resultA, resultB];

            OperationResult globalResult = allResults.Merge();

            //Implement flow based on OperationResult.IsSuccessful
        }

        static async Task<OperationResult> DoSomeWorkAlignedWithResilientPattern(ImALogger logger)
        {
            if (logger is null)
                return OperationResult.Fail($"{nameof(logger)} is null. A logger must be provided.");

            await logger.LogInfo("Doing some work");

            return OperationResult.Win();
        }

        static async Task<OperationResult> WrapSomeWorkWitResilientPattern(ImALogger logger)
        {
            OperationResult result = OperationResult.Fail("Work not yet started");

            await new Func<Task>(async () =>
            {
                await logger.LogInfo("Doing some work");
            })
            .TryOrFailWithGrace(
                onFail: ex => result = OperationResult.Fail(ex, $"Error occurred while trying to do work. Reason: {ex.Message}.")
            );

            return result;
        }
    }
}
