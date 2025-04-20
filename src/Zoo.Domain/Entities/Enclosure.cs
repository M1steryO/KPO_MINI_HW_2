using System;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Enclosure
    {
        public Guid Id { get; private set; }
        public EnclosureType Type { get; private set; }
        public int Size { get; private set; }
        public int CurrentCount => _animals.Count;
        public Capacity Capacity { get; private set; }
        private readonly List<Guid> _animals = new();

        public Enclosure(EnclosureType type, int size, Capacity capacity)
        {
            Id = Guid.NewGuid();
            Type = type;
            Size = size;
            Capacity = capacity;
        }

        public void AddAnimal(Guid animalId)
        {
            if (_animals.Count >= Capacity.Max)
                throw new InvalidOperationException("Enclosure is full.");
            _animals.Add(animalId);
        }

        public void RemoveAnimal(Guid animalId)
        {
            if (!_animals.Remove(animalId))
                throw new InvalidOperationException("Animal not found in enclosure.");
        }

        public void Clean()
        {
            // Cleaning logic
        }
    }
}

