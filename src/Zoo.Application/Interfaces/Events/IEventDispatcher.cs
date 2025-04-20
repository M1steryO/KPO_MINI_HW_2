using System;
namespace Zoo.Application.Interfaces
{
    public interface IEventDispatcher
    {
        void Dispatch<T>(T domainEvent) where T : class;
    }
}

