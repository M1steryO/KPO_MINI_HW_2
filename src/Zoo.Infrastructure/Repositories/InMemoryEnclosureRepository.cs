using System;
using Domain.Entities;
using Zoo.Application.Interfaces;

namespace Zoo.Infrastructure.Repositories
{
    public class InMemoryEnclosureRepository : IEnclosureRepository
    {
        private readonly Dictionary<Guid, Enclosure> _store = new();

        public Enclosure GetById(Guid id)
        {
            if (!_store.TryGetValue(id, out var entity))
                throw new KeyNotFoundException($"Enclosure with id {id} not found.");
            return entity;
        }

        public IEnumerable<Enclosure> GetAll() => _store.Values;

        public void Add(Enclosure entity)
        {
            _store[entity.Id] = entity;
        }

        public void Remove(Guid id)
        {
            if (!_store.Remove(id))
                throw new KeyNotFoundException($"Enclosure with id {id} not found.");
        }
    }
}

