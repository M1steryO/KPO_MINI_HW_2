using System;
using Zoo.Application.Interfaces;
using Zoo.Application.Interfaces.Services;

namespace Zoo.Application.Services
{
    public class ZooStatisticsService : IZooStatisticsService
    {
        private readonly IAnimalRepository _animals;
        private readonly IEnclosureRepository _enclosures;
        private readonly IFeedingScheduleRepository _schedules;

        public ZooStatisticsService(
            IAnimalRepository animals,
            IEnclosureRepository enclosures,
            IFeedingScheduleRepository schedules)
        {
            _animals = animals;
            _enclosures = enclosures;
            _schedules = schedules;
        }

        public int GetTotalAnimals() => _animals.GetAll().Count();
        public int GetFreeEnclosures() => _enclosures.GetAll()
            .Count(e => e.Capacity.Max > e.CurrentCount);
        public int GetPendingFeedings() => _schedules.GetAll()
        .Count(s => !s.Completed);
    }
}

