using System;
using Domain.ValueObjects;

namespace Zoo.Presentation.DTO
{
    public record CreateEnclosureDto(EnclosureType Type, int Size, int Capacity);

}

