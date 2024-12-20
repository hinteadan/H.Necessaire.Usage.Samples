using System;

namespace H.Necessaire.Samples.Serialzation
{
    internal class SomeData
    {
        public Guid ID { get; set; }
        public DateTime AsOf { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public Note[] Attributes { get; set; }
    }
}
