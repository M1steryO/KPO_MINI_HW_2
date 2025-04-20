using System;
using Zoo.Application.Interfaces;

namespace Zoo.Infrastructure.Events
{
    public class SimpleEventDispatcher : IEventDispatcher
    {
        public void Dispatch<T>(T domainEvent) where T : class
        {
            Console.WriteLine($"Event dispatched: {domainEvent.GetType().Name} at {DateTime.UtcNow}");
        }
    }
}

