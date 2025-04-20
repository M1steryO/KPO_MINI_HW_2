# ZooManagement

## 1. Реализованный функционал

| Требуемый функционал                                       | Где реализовано                                                        |
|------------------------------------------------------------|------------------------------------------------------------------------|
| Добавление / удаление животного                           | `AnimalController`, `IAnimalRepository`, `InMemoryAnimalRepository`   |
| Добавление / удаление вольера                              | `EnclosureController`, `IEnclosureRepository`, `InMemoryEnclosureRepository` |
| Перемещение животного между вольерами                     | `AnimalTransferService`, `IAnimalTransferService`, `AnimalMovedEvent` |
| Просмотр расписания кормления                             | `FeedingScheduleController`, `IFeedingOrganizationService`            |
| Добавление кормления в расписание                         | `FeedingScheduleController.Schedule`, `FeedingOrganizationService`     |
| Получение статистики по зоопарку                          | `ZooStatisticsService`, `IZooStatisticsService`                        |

## 2. Применённые концепции DDD и Clean Architecture

### Концепции Domain-Driven Design (DDD)

| Концепция       | Применение                                                               |
|----------------|--------------------------------------------------------------------------|
| Entity          | `Animal`, `Enclosure`, `FeedingSchedule`                                 |
| Value Object    | `Species`, `AnimalName`, `Gender`, `EnclosureType`, `Capacity`           |
| Domain Event    | `AnimalMovedEvent`, `FeedingTimeEvent`                                   |
| Aggregates      | `Animal` как корень агрегата                                             |
| Domain Service  | Методы внутри сущностей инкапсулируют бизнес-логику                     |

### Принципы Clean Architecture

| Принцип                        | Применение                                                                                       |
|-------------------------------|--------------------------------------------------------------------------------------------------|
| Зависимости направлены внутрь | Domain ни от кого не зависит, Application зависит от Domain, Infrastructure зависит от обоих    |
| Абстракции на границах        | Интерфейсы `IRepository<T>`, `IAnimalRepository` и др. находятся в Application                   |
| Реализации во внешнем слое    | In-memory репозитории и EventDispatcher находятся в Infrastructure                              |
| Изоляция бизнес-логики        | `AnimalTransferService`, `FeedingOrganizationService`, `ZooStatisticsService` находятся в Application |
| Контроллеры только UI         | Все контроллеры (`AnimalController`, `EnclosureController`, `FeedingScheduleController`) — в Presentation |

---

