using System;
namespace Zoo.Presentation.DTO
{
    public record FeedingScheduleDto(Guid Id, Guid AnimalId, TimeSpan Time, string FoodType, bool Completed);

}

