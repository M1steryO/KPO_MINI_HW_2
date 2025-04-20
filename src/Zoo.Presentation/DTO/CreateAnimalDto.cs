using System;
using Domain.ValueObjects;

namespace Zoo.Presentation.DTO
{
    public record CreateAnimalDto(string Species, string Name, DateTime BirthDate, Gender Gender, string FavoriteFood);
}

