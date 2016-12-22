namespace vfs2poc.Configuration.Interfaces
{
    public class Cardinality : ICardinality
    {
        public Cardinality() : this(0, int.MaxValue) { }

        public Cardinality(int? min, int? max)
        {
            Min = min ?? 0;
            Max = max ?? int.MaxValue;
        }

        public int? Min { get; set; }

        public int? Max { get; set; }
    }
}
