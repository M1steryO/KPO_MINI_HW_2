using System;
using System.ComponentModel.DataAnnotations;

namespace Zoo.Presentation.Dto
{
    public record CreateAnimalDto(
        [Required, StringLength(100)] string Species,
        [Required, StringLength(50)] string Name,
        [Required] string FavoriteFood,
        Guid? EnclosureId
    );
}
