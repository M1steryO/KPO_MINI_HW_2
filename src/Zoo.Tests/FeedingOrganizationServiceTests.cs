using System.Reflection;
using Domain.Entities;
using Domain.Events;
using Domain.ValueObjects;
using Moq;
using Zoo.Application.Interfaces;
using Zoo.Application.Services;

namespace Zoo.Tests.Application;

public class FeedingOrganizationServiceTests
{
    [Fact]
    public void ScheduleFeeding_AddsNewScheduleToRepository()
    {
        var animalId = Guid.NewGuid();
        var time = new TimeSpan(10, 0, 0);
        var food = "Meat";

        var mockSchedules = new Mock<IFeedingScheduleRepository>();
        var mockAnimals = new Mock<IAnimalRepository>();
        var mockEvents = new Mock<IEventDispatcher>();

        var service = new FeedingOrganizationService(
            mockSchedules.Object,
            mockAnimals.Object,
            mockEvents.Object);

        service.ScheduleFeeding(animalId, time, food);

        // Проверка на то, что метод Add репозитория вызван с правильным расписанием
        mockSchedules.Verify(r => r.Add(
            It.Is<FeedingSchedule>(s =>
                s.AnimalId == animalId &&
                s.FeedingTime == time &&
                s.FoodType == food &&
                !s.Completed)), Times.Once);
    }

    [Fact]
    public void ExecuteFeeding_DueSchedules_FeedAnimalsMarkCompletedAndDispatchEvent()
    {
        var animalId = Guid.NewGuid();
        var scheduleTime = new TimeSpan(8, 0, 0);
        var schedule = new FeedingSchedule(animalId, scheduleTime, "Fruit");

        var mockSchedules = new Mock<IFeedingScheduleRepository>();
        mockSchedules.Setup(r => r.GetAll())
            .Returns(new List<FeedingSchedule> { schedule });

        var animal = new Animal(new Species("Monkey"), new AnimalName("George"), DateTime.Today, Gender.Male, "Fruit");
        var mockAnimals = new Mock<IAnimalRepository>();
        mockAnimals.Setup(r => r.GetById(animalId)).Returns(animal);

        var events = new List<FeedingTimeEvent>();
        var mockEvents = new Mock<IEventDispatcher>();
        mockEvents.Setup(e => e.Dispatch(It.IsAny<FeedingTimeEvent>()))
                  .Callback<object>(evt => events.Add(evt as FeedingTimeEvent));

        var service = new FeedingOrganizationService(
            mockSchedules.Object,
            mockAnimals.Object,
            mockEvents.Object);

        // Предусловие: кормление ещё не отмечено как завершённое
        Assert.False(schedule.Completed);

        service.ExecuteFeeding(new TimeSpan(12, 0, 0));

        // Проверка: кормление отмечено как завершённое
        Assert.True(schedule.Completed);

        // Проверка: репозиторий вызван для сохранения обновлённого расписания
        mockSchedules.Verify(r => r.Add(It.Is<FeedingSchedule>(s => s.Completed)), Times.Once);

        // Проверка: событие с правильными данными отправлено один раз
        Assert.Single(events);
        var dispatched = events.First();
        Assert.Equal(animalId, dispatched.AnimalId);
        Assert.Equal(scheduleTime, dispatched.ScheduledTime);
    }
}