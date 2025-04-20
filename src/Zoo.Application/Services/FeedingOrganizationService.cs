using System;
using Domain.Entities;
using Domain.Events;
using Zoo.Application.Interfaces;
using Zoo.Application.Interfaces.Services;

namespace Zoo.Application.Services
{
    public class FeedingOrganizationService : IFeedingOrganizationService
    {
        private readonly IFeedingScheduleRepository _schedules;
        private readonly IAnimalRepository _animals;
        private readonly IEventDispatcher _events;

        public FeedingOrganizationService(
            IFeedingScheduleRepository schedules,
            IAnimalRepository animals,
            IEventDispatcher events)
        {
            _schedules = schedules;
            _animals = animals;
            _events = events;
        }

        public void ScheduleFeeding(Guid animalId, TimeSpan time, string food)
        {
            var schedule = new FeedingSchedule(animalId, time, food);
            _schedules.Add(schedule);
        }

        public void ExecuteFeeding(TimeSpan now)
        {
            var due = _schedules.GetAll()
                .Where(s => s.FeedingTime <= now && !s.Completed);

            foreach (var s in due)
            {
                var animal = _animals.GetById(s.AnimalId);
                animal.Feed(s.FoodType);
                s.MarkCompleted();

                _events.Dispatch(new FeedingTimeEvent(s.AnimalId, s.FeedingTime));

                _animals.Add(animal);
                _schedules.Add(s);
            }
        }

        public IEnumerable<FeedingSchedule> GetAllSchedules()
        {
            return _schedules.GetAll();
        }

    }
}

