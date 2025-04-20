# ZooManagement

## 1. Реализация функциональных требований

| Use Case                                   | Domain Entity                           | Application Service / Interface               | Infrastructure Repository                    | Presentation Controller                       |
|--------------------------------------------|-----------------------------------------|-----------------------------------------------|----------------------------------------------|-----------------------------------------------|
| Добавить / удалить животное               | `Animal` (Zoo.Domain/Entities/Animal.cs) | `IAnimalRepository` / `AnimalTransferService` | `InMemoryAnimalRepository`                   | `AnimalController` (Zoo.Presentation)         |
| Добавить / удалить вольер                 | `Enclosure` (Zoo.Domain/Entities/Enclosure.cs) | `IEnclosureRepository`                       | `InMemoryEnclosureRepository`                | `EnclosureController` (Zoo.Presentation)      |
| Переместить животное между вольерами      | `Animal`, `Enclosure`                   | `IAnimalTransferService` / `AnimalTransferService` | N/A (через репозитории животных и вольеров) | N/A                                           |
| Просмотреть расписание кормления          | `FeedingSchedule` (Zoo.Domain/Entities/FeedingSchedule.cs) | `IFeedingOrganizationService` / `FeedingOrganizationService` | `InMemoryFeedingScheduleRepository`          | `FeedingScheduleController` (Zoo.Presentation) |
| Добавить новое кормление в расписание     | `FeedingSchedule`                       | `IFeedingOrganizationService`                | `InMemoryFeedingScheduleRepository`          | `FeedingScheduleController.Schedule()`        |
| Просмотреть статистику зоопарка           | -                                    | `IZooStatisticsService` / `ZooStatisticsService` | -                                         | (контроллер не реализован, сервис готов)      |

## 2. Применённые концепции Domain-Driven Design

| Концепция              | Описание                                                             | Класс / Модуль                                     |
|------------------------|----------------------------------------------------------------------|----------------------------------------------------|
| **Сущности (Entities)**      | Бизнес‑объекты с уникальным идентификатором и инкапсулированным поведением | `Animal`, `Enclosure`, `FeedingSchedule` (Zoo.Domain/Entities) |
| **Value Objects**           | Объекты без идентичности, сравнение по значению, неизменяемость       | `Species`, `AnimalName`, `Capacity`, `EnclosureType`, `Gender` (Zoo.Domain/ValueObjects) |
| **Доменные события**        | Факты предметной области для реактивной обработки                     | `AnimalMovedEvent`, `FeedingTimeEvent` (Zoo.Domain/Events) |
| **Репозитории (Interfaces)**| Контракты доступа к данным, изоляция доменной логики от хранилища     | `IAnimalRepository`, `IEnclosureRepository`, `IFeedingScheduleRepository`, `IRepository<T>` (Zoo.Application/Interfaces) |

## 3. Применённые принципы Clean Architecture

| Принцип                                 | Описание                                                                                                             | Реализация (Класс / Модуль)                                                   |
|-----------------------------------------|----------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------|
| **Dependency Inversion Principle (DIP)** | Зависимость от абстракций (интерфейсов), а не от конкретных реализаций                                               | Регистрация сервисов и репозиториев через интерфейсы в DI контейнере (Startup)  |
| **Single Responsibility Principle (SRP)**| Каждый класс отвечает только за одну задачу                                                                          | `AnimalTransferService`, `FeedingOrganizationService`, `ZooStatisticsService`  |
| **Interface Segregation Principle (ISP)**| Специализированные и узконаправленные интерфейсы                                                                      | `IAnimalTransferService`, `IFeedingOrganizationService`, `IZooStatisticsService` |
| **Изоляция слоёв**                      | Слои зависят только внутрь: Presentation → Application → Domain; Domain не знает о Presentation/Infrastructure            | Настройка ссылок между проектами, отсутствие обратных ссылок                    |
| **Use Cases / Application Services**    | Явная инкапсуляция бизнес‑логики конкретных сценариев (юзкейсов)                                                     | `AnimalTransferService`, `FeedingOrganizationService`, `ZooStatisticsService`  |
| **Infrastructure Implementations**      | Отделение технических деталей (хранение, диспатчинг событий) от бизнес‑логики                                          | `InMemory*Repository` (Zoo.Infrastructure/Repositories), `SimpleEventDispatcher` (Zoo.Infrastructure/Events) |

