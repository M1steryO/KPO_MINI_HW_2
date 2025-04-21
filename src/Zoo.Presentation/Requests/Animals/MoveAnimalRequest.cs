using System;
using System.ComponentModel.DataAnnotations;

namespace Zoo.Presentation.Dto
{
    public record MoveAnimalDto(
        [Required] Guid NewEnclosureId
    );
}
