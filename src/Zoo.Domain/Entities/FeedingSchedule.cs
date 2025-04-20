using System;
namespace Domain.Entities
{
    public class FeedingSchedule
    {
        public Guid Id { get; private set; }
        public Guid AnimalId { get; private set; }
        public TimeSpan FeedingTime { get; private set; }
        public string FoodType { get; private set; }
        public bool Completed { get; private set; }

        public FeedingSchedule(Guid animalId, TimeSpan time, string foodType)
        {
            Id = Guid.NewGuid();
            AnimalId = animalId;
            FeedingTime = time;
            FoodType = foodType;
            Completed = false;
        }

        public void Reschedule(TimeSpan newTime)
        {
            FeedingTime = newTime;
        }

        public void MarkCompleted()
        {
            Completed = true;
        }
    }
}

