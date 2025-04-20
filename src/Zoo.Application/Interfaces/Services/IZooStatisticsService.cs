using System;
namespace Zoo.Application.Interfaces.Services
{
    public interface IZooStatisticsService
    {
        int GetTotalAnimals();
        int GetFreeEnclosures();
        int GetPendingFeedings();
    }
}

