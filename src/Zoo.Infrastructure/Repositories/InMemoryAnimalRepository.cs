using System;
using Domain.Entities;
using Zoo.Application.Interfaces;

namespace Zoo.Infrastructure.Repositories
{
    public class InMemoryAnimalRepository : IAnimalRepository
    {
        private readonly Dictionary<Guid, Animal> _store = new();

        public Animal GetById(Guid id)
        {
            if (!_store.TryGetValue(id, out var entity))
                throw new KeyNotFoundException($"Animal with id {id} not found.");
            return entity;
        }

        public IEnumerable<Animal> GetAll() => _store.Values;

        public void Add(Animal entity)
        {
            _store[entity.Id] = entity;
        }

        public void Remove(Guid id)
        {
            if (!_store.Remove(id))
                throw new KeyNotFoundException($"Animal with id {id} not found.");
        }
    }
}

