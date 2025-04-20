using System;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Animal
    {
        public Guid Id { get; private set; }
        public Species Species { get; private set; }
        public AnimalName Name { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }
        public string FavoriteFood { get; private set; }
        public bool IsHealthy { get; private set; }
        public Guid? EnclosureId { get; private set; }

        public Animal(Species species, AnimalName name, DateTime birthDate, Gender gender, string favoriteFood)
        {
            Id = Guid.NewGuid();
            Species = species;
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
            FavoriteFood = favoriteFood;
            IsHealthy = true;
        }

        public void Feed(string food)
        {
            // Business rule: only feed favorite food
            if (food != FavoriteFood)
                throw new InvalidOperationException("Cannot feed: food not favorite.");
            // trigger FeedingTimeEvent via domain service
        }

        public void Treat()
        {
            IsHealthy = true;
        }

        public void AssignToEnclosure(Guid enclosureId)
        {
            EnclosureId = enclosureId;
        }
    }
}

