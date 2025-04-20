using System;
using Domain.Entities;
using Zoo.Application.Interfaces;

namespace Zoo.Infrastructure.Repositories
{
    public class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
    {
        private readonly Dictionary<Guid, FeedingSchedule> _store = new();

        public FeedingSchedule GetById(Guid id)
        {
            if (!_store.TryGetValue(id, out var entity))
                throw new KeyNotFoundException($"Schedule with id {id} not found.");
            return entity;
        }

        public IEnumerable<FeedingSchedule> GetAll() => _store.Values;

        public void Add(FeedingSchedule entity)
        {
            _store[entity.Id] = entity;
        }

        public void Remove(Guid id)
        {
            if (!_store.Remove(id))
                throw new KeyNotFoundException($"Schedule with id {id} not found.");
        }
    }
}

