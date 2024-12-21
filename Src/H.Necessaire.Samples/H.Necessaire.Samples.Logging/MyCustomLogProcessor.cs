using System;
using System.Threading.Tasks;

namespace H.Necessaire.Samples.Logging
{
    internal class MyCustomLogProcessor : ImALogProcessor
    {
        public ImALogProcessor ConfigWith(LogConfig logConfig) => this;

        public LoggerPriority GetPriority() => LoggerPriority.Immediate;

        public Task<bool> IsEligibleFor(LogEntry logEntry) => true.AsTask();

        public Task<OperationResult<LogEntry>> Process(LogEntry logEntry)
        {
            Console.WriteLine("~~~CustomLogProcessor BEGIN~~~");
            Console.WriteLine(logEntry.Message);
            Console.WriteLine("~~~CustomLogProcessor END~~~");
            return OperationResult.Win().WithPayload(logEntry).AsTask();
        }
    }
}
