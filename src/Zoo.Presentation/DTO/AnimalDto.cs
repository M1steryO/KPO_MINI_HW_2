using Domain.ValueObjects;

namespace Zoo.Presentation.DTO
{
    public record AnimalDto(Guid Id, string Species, string Name, DateTime BirthDate, Gender Gender, string FavoriteFood, bool IsHealthy, Guid? EnclosureId);
}