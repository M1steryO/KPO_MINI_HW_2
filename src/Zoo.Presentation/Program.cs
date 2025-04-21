using Microsoft.OpenApi.Models;
using Zoo.Application.Interfaces;
using Zoo.Application.Services;
using Zoo.Infrastructure.Repositories;
using Zoo.Infrastructure.Events;
using Domain.Entities;
using Domain.ValueObjects;
using Zoo.Application.Interfaces.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IAnimalRepository, InMemoryAnimalRepository>()
    .AddSingleton<IEnclosureRepository, InMemoryEnclosureRepository>()
    .AddSingleton<IFeedingScheduleRepository, InMemoryFeedingScheduleRepository>()
    .AddSingleton<IEventDispatcher, SimpleEventDispatcher>()
    .AddScoped<IAnimalTransferService, AnimalTransferService>()
    .AddScoped<IFeedingOrganizationService, FeedingOrganizationService>()
    .AddScoped<IZooStatisticsService, ZooStatisticsService>()
    .AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Zoo API",
        Version = "v1",
        Description = "API для управления зоопарком"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zoo API v1"));
}

app.UseHttpsRedirection();
app.MapControllers();

await InitializeTestData(app.Services);

app.Run();

async Task InitializeTestData(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var animalRepo = scope.ServiceProvider.GetRequiredService<IAnimalRepository>();
    var enclosureRepo = scope.ServiceProvider.GetRequiredService<IEnclosureRepository>();

    var carnivore = new Enclosure(EnclosureType.Carnivore, 100, new Capacity(5));
    var herbivore = new Enclosure(EnclosureType.Herbivore, 200, new Capacity(10));

    enclosureRepo.Add(carnivore);
    enclosureRepo.Add(herbivore);

    var tiger = new Animal(
        new Species("Tiger"),
        new AnimalName("Sherkhan"),
        DateTime.Today.AddYears(-4),
        Gender.Male,
        "Meat");
    tiger.AssignToEnclosure(carnivore.Id);

    var rabbit = new Animal(
        new Species("Rabbit"),
        new AnimalName("Snowball"),
        DateTime.Today.AddYears(-2),
        Gender.Female,
        "Plants");
    rabbit.AssignToEnclosure(herbivore.Id);

    animalRepo.Add(tiger);
    animalRepo.Add(rabbit);
}
