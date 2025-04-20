# Отчёт по реализации приложения ZooManagement

## 1. Реализованный функционал

Ниже перечислены пункты из требуемого функционала и места их реализации в коде:

| Функционал                                                | Слой / Модуль                                | Класс / Файл                                            |
|-----------------------------------------------------------|----------------------------------------------|---------------------------------------------------------|
| a. Добавить / удалить животное                            | Presentation → Controllers                   | `AnimalController.Create`, `AnimalController.Delete`    |
|                                                           | Application → Services                       | `IAnimalRepository`, `InMemoryAnimalRepository`         |
| b. Добавить / удалить вольер                               | Presentation → Controllers                   | `EnclosureController.Create`, `EnclosureController.Delete` |
|                                                           | Application → Services                       | `IEnclosureRepository`, `InMemoryEnclosureRepository`   |
| c. Переместить животное между вольерами                   | Application → Services                       | `AnimalTransferService.Transfer`                        |
|                                                           | Application → Interfaces                      | `IAnimalTransferService`                                |
|                                                           | Domain → Events                               | `AnimalMovedEvent`                                       |
| d. Просмотреть расписание кормления                       | Presentation → Controllers                   | `FeedingScheduleController.GetAll`                      |
|                                                           | Application → Interfaces                      | `IFeedingOrganizationService.GetAllSchedules`           |
|                                                           | Infrastructure → Repositories                  | `InMemoryFeedingScheduleRepository`                     |
| e. Добавить новое кормление в расписание                  | Presentation → Controllers                   | `FeedingScheduleController.Schedule`                    |
|                                                           | Application → Services                        | `FeedingOrganizationService.ScheduleFeeding`            |
| f. Просмотреть статистику зоопарка                        | Application → Services                        | `ZooStatisticsService.GetTotalAnimals`, `GetFreeEnclosures`, `GetPendingFeedings` |
|                                                           | Application → Interfaces                      | `IZooStatisticsService`                                 |

> **Примечание:** контроллер для статистики (`StatisticsController`) можно добавить аналогично остальным для полноценного REST API.

## 2. Применённые концепции Domain‑Driven Design и принципы Clean Architecture

| Концепция / Принцип                      | Описание / Зачем                        | Слои / Классы                                  |
|------------------------------------------|-----------------------------------------|------------------------------------------------|
| **Value Object**                         | Инкапсуляция примитивных полей + валидация<br>Сравнение по значению | `Domain/ValueObjects/Species`, `AnimalName`, `Capacity`, `EnclosureType`, `Gender` |
| **Entity**                              | Объекты с собственной идентичностью     | `Domain/Entities/Animal`, `Enclosure`, `FeedingSchedule` |
| **Aggregate Root**                      | Ядро агрегации (группировка сущностей)  | `Animal` (Enclosure через ссылку), `Enclosure`, `FeedingSchedule` |
| **Domain Event**                        | Сообщение о факте изменения в предметной области | `Domain/Events/AnimalMovedEvent`, `FeedingTimeEvent` |
| **Repository**                           | Абстракция доступа к данным             | `Application/Interfaces/IRepository<T>`,<br>`IAnimalRepository`, `IEnclosureRepository`, `IFeedingScheduleRepository` |
| **Service (Domain/Application)**        | Бизнес‑логика / организация use cases    | `AnimalTransferService`, `FeedingOrganizationService`, `ZooStatisticsService` |
| **Dependency Inversion Principle (DIP)**| Слой Infrastructure зависит от интерфейсов Application; Presentation → Application; Application → Domain; Domain не зависит ни от кого | Все интерфейсы в `Zoo.Application.Interfaces`;<br>реализации в `Zoo.Infrastructure` и контроллеры в `Zoo.Presentation` |
| **Separation of Concerns (SoC)**        | Чёткое разделение ответсвенности по слоям | Domain, Application, Infrastructure, Presentation  |
| **Single Responsibility Principle (SRP)**| Каждый класс выполняет одну задачу        | Каждый сервис/репозиторий/контроллер отдельный класс  |
| **Interface Segregation Principle (ISP)**| Интерфейсы узко специализированы       | `IAnimalRepository`, `IFeedingOrganizationService`, `IZooStatisticsService` и пр. |
| **Open/Closed Principle (OCP)**         | Возможность расширения без изменения кода | Создание новых репозиториев/декораторов через имплементацию интерфейсов  |

---

