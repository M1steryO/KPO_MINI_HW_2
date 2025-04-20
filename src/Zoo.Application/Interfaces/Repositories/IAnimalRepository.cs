using System;
using Domain.Entities;

namespace Zoo.Application.Interfaces
{
    public interface IAnimalRepository : IRepository<Animal>
    {
    }
}

