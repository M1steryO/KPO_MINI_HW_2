using System;
namespace Zoo.Application.Interfaces.Services
{
    public interface IAnimalTransferService
    {
        void Transfer(Guid animalId, Guid toEnclosureId);
    }
}

