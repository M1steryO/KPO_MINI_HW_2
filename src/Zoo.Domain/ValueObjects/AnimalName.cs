using System;
namespace Domain.ValueObjects
{
    public sealed class AnimalName
    {
        public string Value { get; }
        public AnimalName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Animal name cannot be empty.", nameof(value));
            Value = value;
        }
        public override bool Equals(object obj) => obj is AnimalName other && Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value;
    }
}

