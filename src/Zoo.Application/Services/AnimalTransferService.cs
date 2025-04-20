using System;
using Domain.Events;
using Zoo.Application.Interfaces;
using Zoo.Application.Interfaces.Services;

namespace Zoo.Application.Services
{
    public class AnimalTransferService : IAnimalTransferService
    {
        private readonly IAnimalRepository _animals;
        private readonly IEnclosureRepository _enclosures;
        private readonly IEventDispatcher _events;

        public AnimalTransferService(
            IAnimalRepository animals,
            IEnclosureRepository enclosures,
            IEventDispatcher events)
        {
            _animals = animals;
            _enclosures = enclosures;
            _events = events;
        }

        public void Transfer(Guid animalId, Guid toEnclosureId)
        {
            var animal = _animals.GetById(animalId);
            if (animal.EnclosureId.HasValue)
            {
                var fromId = animal.EnclosureId.Value;
                var fromEnclosure = _enclosures.GetById(fromId);

                fromEnclosure.RemoveAnimal(animalId);
                _enclosures.Add(fromEnclosure); // save

                animal.AssignToEnclosure(toEnclosureId);
                var toEnclosure = _enclosures.GetById(toEnclosureId);
                toEnclosure.AddAnimal(animalId);
                _enclosures.Add(toEnclosure); // save

                var evt = new AnimalMovedEvent(animalId, fromId, toEnclosureId);
                _events.Dispatch(evt);
            }
            else
            {
                animal.AssignToEnclosure(toEnclosureId);
                var toEnclosure = _enclosures.GetById(toEnclosureId);

                toEnclosure.AddAnimal(animalId);
                _events.Dispatch(new AnimalMovedEvent(animalId, Guid.Empty, toEnclosureId));
            }

            _animals.Add(animal); // save
        }
    }
}

