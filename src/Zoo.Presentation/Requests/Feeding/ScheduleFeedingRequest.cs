using System;
using System.ComponentModel.DataAnnotations;

namespace Zoo.Presentation.Dto
{
    public record ScheduleFeedingDto(
        [Required] Guid AnimalId,
        [Required] TimeSpan Time,
        [Required] string FoodType
    );
}
