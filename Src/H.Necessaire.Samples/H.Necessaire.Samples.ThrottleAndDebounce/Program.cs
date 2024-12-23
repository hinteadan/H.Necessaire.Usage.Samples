using System;
using System.Threading.Tasks;

namespace H.Necessaire.Samples.ThrottleAndDebounce
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            HighFrequencyEventDaemon highFrequencyEventDaemon = IoC.NewDependencyRegistry()
                .Register<HNecessaireDependencyGroup>(() => new HNecessaireDependencyGroup())
                .Register<HighFrequencyEventDaemon>(() => new HighFrequencyEventDaemon())
                .Get<HighFrequencyEventDaemon>()
                ;

            highFrequencyEventDaemon.OnHighFrequencyEvent += NormalExecution;
            highFrequencyEventDaemon.OnHighFrequencyEvent += ThrottledExecution;
            highFrequencyEventDaemon.OnHighFrequencyEvent += DebouncedExecution;

            Console.WriteLine("Running... press Ctrl+C to stop.");

            await highFrequencyEventDaemon.Start();

            StopDaemonAfter(highFrequencyEventDaemon, TimeSpan.FromSeconds(3));            

            await KeepConsoleAlive();
        }

        static async Task NormalExecution(object sender, EventArgs ev)
        {
            await Console.Out.WriteAsync('_');
        }

        static readonly Throttler throttler = new Throttler(async () => await Console.Out.WriteAsync("_Tht"), TimeSpan.FromSeconds(.35));
        static async Task ThrottledExecution(object sender, EventArgs ev)
        {
            await throttler.Invoke();
        }

        static readonly Debouncer debouncer = new Debouncer(async () => await Console.Out.WriteAsync("_Dbc"), TimeSpan.FromSeconds(.35));
        static async Task DebouncedExecution(object sender, EventArgs ev)
        {
            await debouncer.Invoke();
        }

        static void StopDaemonAfter(ImADaemon daemon, TimeSpan timeSpan)
        {
            Task.Run(async () =>
            {
                await Task.Delay(timeSpan);
                await daemon.Stop();
            });
        }

        static Task KeepConsoleAlive()
        {
            TaskCompletionSource taskCompletionSource = new TaskCompletionSource();

            Console.CancelKeyPress += (sender, ev) =>
            {
                taskCompletionSource.SetResult();
                Console.WriteLine($"Done @ {DateTime.Now}");
            };

            return taskCompletionSource.Task;
        }
    }
}
