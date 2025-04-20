using System;
namespace Zoo.Presentation.DTO
{
    public record ScheduleFeedingDto(Guid AnimalId, TimeSpan Time, string FoodType);
}

