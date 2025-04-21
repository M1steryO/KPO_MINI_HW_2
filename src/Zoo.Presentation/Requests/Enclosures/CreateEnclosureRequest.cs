using System;
using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;

namespace Zoo.Presentation.Dto
{
    public record CreateEnclosureDto(
        [Required] string Type,        
        [Range(1, 1000)] double Size,
        [Range(1, 50)] int MaxCapacity)
    {
        public EnclosureType GetEnclosureType()
        {
            return Type.ToLowerInvariant() switch
            {
                "carnivore" => EnclosureType.Carnivore,
                "herbivore" => EnclosureType.Herbivore,
                "aviary" => EnclosureType.Aviary,
                "aquarium" => EnclosureType.Aquarium,
                "mixed" => EnclosureType.Mixed,
                _ => throw new ValidationException($"Unknown EnclosureType: '{Type}'. Allowed values: Carnivore, Herbivore, Aviary, Aquarium, Mixed.")
            };
        }
    }
}
