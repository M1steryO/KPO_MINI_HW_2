using System;
using Domain.Entities;

namespace Zoo.Application.Interfaces.Services
{
    public interface IFeedingOrganizationService
    {

        void ScheduleFeeding(Guid animalId, TimeSpan time, string food);
        void ExecuteFeeding(TimeSpan now);
        IEnumerable<FeedingSchedule> GetAllSchedules();
    }
}

