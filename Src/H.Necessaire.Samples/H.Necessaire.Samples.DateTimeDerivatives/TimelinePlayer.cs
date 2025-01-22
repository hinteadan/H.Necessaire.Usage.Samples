using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Necessaire.Samples.DateTimeDerivatives
{
    internal class TimelinePlayer
    {
        readonly PeriodOfTime periodOfTime;
        readonly TimeSpan increment = TimeSpan.FromSeconds(1);
        readonly TimeSpan delayBetweenRounds = TimeSpan.Zero;

        public TimelinePlayer(PeriodOfTime periodOfTime, TimeSpan increment, TimeSpan delayBetweenRounds)
        {
            if (periodOfTime.IsInfinite)
                throw new ArgumentException("Period of time must be finite", nameof(periodOfTime));

            this.periodOfTime = periodOfTime;
            this.increment = increment;
            this.delayBetweenRounds = delayBetweenRounds;
        }
        public TimelinePlayer(PeriodOfTime periodOfTime, TimeSpan increment) : this(periodOfTime, increment, TimeSpan.Zero) { }
        public TimelinePlayer(PeriodOfTime periodOfTime) : this(periodOfTime, TimeSpan.FromSeconds(1), TimeSpan.Zero) { }

        public async Task Play(Func<DateTime, CancellationToken, Task> roundAction, CancellationToken cancellationToken)
        {
            DateTime now = periodOfTime.From.Value;

            while (now <= periodOfTime.To.Value)
            {
                cancellationToken.ThrowIfCancellationRequested();

                await Task.Delay(delayBetweenRounds, cancellationToken);

                await roundAction(now, cancellationToken);

                now += increment;
            }
        }
        public Task Play(Func<DateTime, Task> roundAction) => Play(async (now, token) => await roundAction(now), CancellationToken.None);
    }
}
