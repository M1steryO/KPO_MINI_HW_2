using Domain.Entities;

namespace Zoo.Presentation.Responses;

public record AnimalResponse(
    Guid Id,
    string Species,
    string Name,
    string FoodType,
    Guid? EnclosureId,
    string? EnclosureName = null)
{
    public static AnimalResponse FromDomain(Animal animal, string? enclosureName = null) => new(
        Id: animal.Id,
        Species: animal.Species.Value,
        Name: animal.Name.Value,
        FoodType: animal.FavoriteFood,
        EnclosureId: animal.EnclosureId,
        EnclosureName: enclosureName);
}