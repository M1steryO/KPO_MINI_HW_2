using System;
namespace Domain.ValueObjects
{
    public sealed class Capacity
    {
        public int Max { get; }
        public Capacity(int max)
        {
            if (max < 1)
                throw new ArgumentException("Capacity must be at least 1.", nameof(max));
            Max = max;
        }
        public override bool Equals(object obj) => obj is Capacity other && Max == other.Max;
        public override int GetHashCode() => Max.GetHashCode();
        public override string ToString() => Max.ToString();
    }
}

