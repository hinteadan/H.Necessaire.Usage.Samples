using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace H.Necessaire.Samples.ThrottleAndDebounce
{
    internal class HighFrequencyEventDaemon : ImADaemon, ImADependency
    {
        static readonly TimeSpan eventFrequency = TimeSpan.FromMilliseconds(15);
        readonly List<AsyncEventHandler<EventArgs>> onHighFrequencyEventHandlers = new List<AsyncEventHandler<EventArgs>>();
        public event AsyncEventHandler<EventArgs> OnHighFrequencyEvent
        {
            add
            {
                if (value is null)
                    return;

                lock (onHighFrequencyEventHandlers)
                {
                    onHighFrequencyEventHandlers.Add(value);
                }
            }
            remove
            {
                if (value is null)
                    return;

                lock (onHighFrequencyEventHandlers)
                {
                    onHighFrequencyEventHandlers.Remove(value);
                }
            }
        }
        ImAPeriodicAction frequentEventPeriodicAction;
        public void ReferDependencies(ImADependencyProvider dependencyProvider)
        {
            frequentEventPeriodicAction = dependencyProvider.Get<ImAPeriodicAction>();
        }

        public Task Start(CancellationToken? cancellationToken = null)
        {
            frequentEventPeriodicAction.Start(eventFrequency, RaiseHighFrequencyEvent);

            return Task.CompletedTask;
        }

        public Task Stop(CancellationToken? cancellationToken = null)
        {
            frequentEventPeriodicAction?.Stop();

            return Task.CompletedTask;
        }

        async Task RaiseHighFrequencyEvent()
        {
            await
                new Func<Task>(async () =>
                {
                    if (onHighFrequencyEventHandlers.IsEmpty())
                        return;

                    await Task.WhenAll(
                        onHighFrequencyEventHandlers.Select(handler =>
                            handler.Invoke(
                                this,
                                EventArgs.Empty
                            )
                        )
                    );
                })
                .TryOrFailWithGrace(onFail: ex => { });
        }


    }
}
