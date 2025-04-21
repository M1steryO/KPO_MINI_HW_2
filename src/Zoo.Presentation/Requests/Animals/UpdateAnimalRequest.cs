using System.ComponentModel.DataAnnotations;

namespace Zoo.Presentation.Dto
{
    public record UpdateAnimalDto(
        [StringLength(100)] string? Species,
        [StringLength(50)] string? Name,
        string? FavoriteFood
    );
}
