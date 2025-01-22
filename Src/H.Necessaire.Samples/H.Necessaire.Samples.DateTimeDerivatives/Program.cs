using System;
using System.Threading.Tasks;

namespace H.Necessaire.Samples.DateTimeDerivatives
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await RunDailyAlarmSampleViaPartialDateTime();
            UpdateConsoleLine();

            await RunBusinessHoursSampleViaPartialPeriodOfTime();
            UpdateConsoleLine();

            await RunRoadtripPlanningSampleViaApproximatePeriodOfTime();
            UpdateConsoleLine();
        }

        static async Task RunDailyAlarmSampleViaPartialDateTime()
        {
            // Easily configure a daily alarm via PartialDateTime
            PartialDateTime dailyAlarm = new PartialDateTime { Hour = 10, Minute = 30, DateTimeKind = DateTimeKind.Local, };


            //And here's how it's being used; 


            TimelinePlayer timelinePlayer = new TimelinePlayer(
                periodOfTime: (DateTime.Today.AddDays(-2), DateTime.Today.AddDays(2).AddMilliseconds(-1)),
                increment: TimeSpan.FromMinutes(30),
                delayBetweenRounds: TimeSpan.FromSeconds(.1)
            );

            await timelinePlayer.Play(async now =>
            {

                UpdateConsoleLine(now.PrintDateAndTime());

                if (dailyAlarm.IsMatchingDateTime(now))//This is the key line of code that checks if the current time matches the daily alarm
                {
                    using (new Highlight()) UpdateConsoleLine($"⏰🔔  DAILY ALARM ringing on {now.PrintDateAndTime()}");

                    await Task.Delay(TimeSpan.FromSeconds(2));
                }

            });
        }

        static async Task RunBusinessHoursSampleViaPartialPeriodOfTime()
        {
            // Easily configure the business hours via PartialPeriodOfTime
            PartialPeriodOfTime businessHours = new PartialPeriodOfTime
            {
                From = new PartialDateTime { Hour = 9, Minute = 0, DateTimeKind = DateTimeKind.Local, },
                To = new PartialDateTime { Hour = 21, Minute = 0, DateTimeKind = DateTimeKind.Local, },
            }
            .OnWeekDays([DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday])
            ;


            //And here's how it's being used; 


            TimelinePlayer timelinePlayer = new TimelinePlayer(
                periodOfTime: (DateTime.Today.AddDays(-14), DateTime.Today.AddDays(2).AddMilliseconds(-1)),
                increment: TimeSpan.FromHours(1),
                delayBetweenRounds: TimeSpan.FromSeconds(.15)
            );

            await timelinePlayer.Play(now =>
            {

                if (businessHours.OnDateAndTime(now).IsSurelyInactive(asOf: now))
                    using (new Highlight(ConsoleColor.Red)) UpdateConsoleLine($"⛔  CLOSED on {now.PrintDateAndTime()}");

                if (businessHours.OnDateAndTime(now).IsSurelyActive(asOf: now))
                    using (new Highlight(ConsoleColor.Green)) UpdateConsoleLine($"✅  OPEN on {now.PrintDateAndTime()}");

                return Task.CompletedTask;
            });
        }

        static async Task RunRoadtripPlanningSampleViaApproximatePeriodOfTime()
        {
            //Plan a roadtrip for the summer, that will start sometime in April and end sometime in August
            ApproximatePeriodOfTime roadtripPlanning = new ApproximatePeriodOfTime
            {
                StartPeriod = new PartialDateTime { Year = DateTime.Today.Year, Month = 4, DateTimeKind = DateTimeKind.Local, },
                EndPeriod = new PartialDateTime { Year = DateTime.Today.Year, Month = 8, DateTimeKind = DateTimeKind.Local, },
            };

            TimelinePlayer timelinePlayer = new TimelinePlayer(
                periodOfTime: (new DateTime(DateTime.Today.Year, 2, 15), new DateTime(DateTime.Today.Year, 9, 20)),
                increment: TimeSpan.FromDays(1),
                delayBetweenRounds: TimeSpan.FromSeconds(.15)
            );

            await timelinePlayer.Play(now =>
            {

                if (now < roadtripPlanning)
                    using (new Highlight(ConsoleColor.Red)) UpdateConsoleLine($"⛔🚗 NOT YET DEPARTED on {now.PrintDateAndTime()}");

                else if (roadtripPlanning.HasPossiblyStarted(asOf: now) && !roadtripPlanning.HasSurelyStarted(asOf: now))
                    using (new Highlight(ConsoleColor.Yellow)) UpdateConsoleLine($"⚙️🚗 PLANNING on {now.PrintDateAndTime()}");

                else if (roadtripPlanning.IsSurelyActive(asOf: now))
                    using (new Highlight(ConsoleColor.Green)) UpdateConsoleLine($"🚗 ROADTRIPPING on {now.PrintDateAndTime()}");

                else if (roadtripPlanning.HasPossiblyEnded(asOf: now) && !roadtripPlanning.HasSurelyEnded(asOf: now))
                    using (new Highlight(ConsoleColor.Yellow)) UpdateConsoleLine($"⚙️🚗 RETURNING on {now.PrintDateAndTime()}");

                else if (now > roadtripPlanning)
                    using (new Highlight(ConsoleColor.Red)) UpdateConsoleLine($"⛔🚗 DONE on {now.PrintDateAndTime()}");

                return Task.CompletedTask;
            });
        }

        static void UpdateConsoleLine(string newText = "")
        {
            Console.SetCursorPosition(0, Console.CursorTop == 0 ? 0 : Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine(newText);
        }
    }
}
