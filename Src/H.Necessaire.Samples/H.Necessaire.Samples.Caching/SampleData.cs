using System;

namespace H.Necessaire.Samples.Caching
{
    internal class SampleData : IGuidIdentity
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public DateTime AsOf { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public Note[] Attributes { get; set; }

        public VersionNumber Version { get; set; } = new VersionNumber();

        public override string ToString()
        {
            return $"{(Name.IsEmpty() ? ID : Name)} v{Version?.Semantic ?? "N/A"}";
        }
    }
}
