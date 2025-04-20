using System;
using Domain.ValueObjects;

namespace Zoo.Presentation.DTO
{
    public record EnclosureDto(Guid Id, EnclosureType Type, int Size, int CurrentCount, int Capacity);

}

