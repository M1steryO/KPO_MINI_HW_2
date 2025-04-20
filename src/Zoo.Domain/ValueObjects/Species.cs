using System;
namespace Domain.ValueObjects
{
    public sealed class Species
    {
        public string Value { get; }
        public Species(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Species cannot be empty.", nameof(value));
            Value = value;
        }
        public override bool Equals(object obj) => obj is Species other && Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value;
    }
}

