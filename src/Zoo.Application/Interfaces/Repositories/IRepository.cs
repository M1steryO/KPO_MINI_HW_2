using System;
namespace Zoo.Application.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Remove(Guid id);
    }
}

