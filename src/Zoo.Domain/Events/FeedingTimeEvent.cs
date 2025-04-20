using System;
namespace Domain.Events
{
    public class FeedingTimeEvent
    {
        public Guid AnimalId { get; }
        public TimeSpan ScheduledTime { get; }
        public DateTime OccurredAt { get; }

        public FeedingTimeEvent(Guid animalId, TimeSpan scheduledTime)
        {
            AnimalId = animalId;
            ScheduledTime = scheduledTime;
            OccurredAt = DateTime.UtcNow;
        }
    }
}

