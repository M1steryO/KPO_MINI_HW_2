using System.Reflection;
using Domain.Entities;
using Domain.ValueObjects;
using Moq;
using Zoo.Application.Interfaces;
using Zoo.Application.Services;

namespace Zoo.Tests.Application;

public class ZooStatisticsServiceTests
{
    [Fact]
    public void GetTotalAnimals_ReturnsCorrectCount()
    {
        var animals = new List<Animal>
            {
                new Animal(new Species("Lion"), new AnimalName("Leo"), DateTime.Today, Gender.Male, "Meat"),
                new Animal(new Species("Tiger"), new AnimalName("Tigra"), DateTime.Today, Gender.Female, "Meat")
            };
        var mockAnimals = new Mock<IAnimalRepository>();
        mockAnimals.Setup(r => r.GetAll()).Returns(animals);

        var mockEnclosures = new Mock<IEnclosureRepository>();
        mockEnclosures.Setup(r => r.GetAll()).Returns(new List<Enclosure>());

        var mockSchedules = new Mock<IFeedingScheduleRepository>();
        mockSchedules.Setup(r => r.GetAll()).Returns(new List<FeedingSchedule>());

        var service = new ZooStatisticsService(
            mockAnimals.Object,
            mockEnclosures.Object,
            mockSchedules.Object);

        var total = service.GetTotalAnimals();

        // Проверка: количество животных корректно
        Assert.Equal(2, total);
    }

    [Fact]
    public void GetFreeEnclosures_ReturnsCountOfEnclosuresWithAvailableCapacity()
    {
        var emptyEnclosure = new Enclosure(EnclosureType.Herbivore, 100, new Capacity(5));
        var fullEnclosure = new Enclosure(EnclosureType.Carnivore, 50, new Capacity(1));
        fullEnclosure.AddAnimal(Guid.NewGuid()); // теперь вольер заполнен

        var mockAnimals = new Mock<IAnimalRepository>();
        mockAnimals.Setup(r => r.GetAll()).Returns(new List<Animal>());

        var mockEnclosures = new Mock<IEnclosureRepository>();
        mockEnclosures.Setup(r => r.GetAll()).Returns(new List<Enclosure> { emptyEnclosure, fullEnclosure });

        var mockSchedules = new Mock<IFeedingScheduleRepository>();
        mockSchedules.Setup(r => r.GetAll()).Returns(new List<FeedingSchedule>());

        var service = new ZooStatisticsService(
            mockAnimals.Object,
            mockEnclosures.Object,
            mockSchedules.Object);

        var free = service.GetFreeEnclosures();

        // Проверка: только пустой вольер считается свободным
        Assert.Equal(1, free);
    }

    [Fact]
    public void GetPendingFeedings_ReturnsCountOfNotCompletedSchedules()
    {
        var done = new FeedingSchedule(Guid.NewGuid(), TimeSpan.Zero, "Food");
        done.MarkCompleted();
        var pending = new FeedingSchedule(Guid.NewGuid(), TimeSpan.Zero, "Food");

        var mockAnimals = new Mock<IAnimalRepository>();
        mockAnimals.Setup(r => r.GetAll()).Returns(new List<Animal>());

        var mockEnclosures = new Mock<IEnclosureRepository>();
        mockEnclosures.Setup(r => r.GetAll()).Returns(new List<Enclosure>());

        var mockSchedules = new Mock<IFeedingScheduleRepository>();
        mockSchedules.Setup(r => r.GetAll()).Returns(new List<FeedingSchedule> { done, pending });

        var service = new ZooStatisticsService(
            mockAnimals.Object,
            mockEnclosures.Object,
            mockSchedules.Object);

        var pendingCount = service.GetPendingFeedings();

        // Проверка: только одно незавершённое кормление
        Assert.Equal(1, pendingCount);
    }
}
