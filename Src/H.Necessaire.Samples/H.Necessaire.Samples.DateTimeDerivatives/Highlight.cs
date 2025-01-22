using System;

namespace H.Necessaire.Samples.DateTimeDerivatives
{
    internal class Highlight : ScopedRunner
    {
        public Highlight(ConsoleColor color = ConsoleColor.Yellow)
        : base(
              onStart: () => Console.ForegroundColor = color,
              onStop: () => Console.ResetColor()
        )
        { }
    }
}
