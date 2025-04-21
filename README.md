# ZooManagement

## 1. Реализация функциональных требований

**Use Cases:**

1. **Добавить / удалить животное**
   - **Контроллер**: `AnimalController` (`Create` и `Delete` методы) в проекте **Zoo.Presentation/Controllers**
   - **Entity**: `Animal` в **Zoo.Domain/Entities/Animal.cs**
   - **Infrastructure**: `InMemoryAnimalRepository` в **Zoo.Infrastructure/Repositories/InMemoryAnimalRepository.cs**

2. **Добавить / удалить вольер**
   - **Контроллер**: `EnclosureController` (`Create` и `Delete` методы) в **Zoo.Presentation/Controllers/EnclosureController.cs**
   - **Entity**: `Enclosure` в **Zoo.Domain/Entities/Enclosure.cs**
   - **Infrastructure**: `InMemoryEnclosureRepository` в **Zoo.Infrastructure/Repositories/InMemoryEnclosureRepository.cs**

3. **Переместить животное между вольерами**
   - **Сервис**: `AnimalTransferService` (реализация `IAnimalTransferService`) в **Zoo.Application/Services/AnimalTransferService.cs**
   - **Доменное событие**: `AnimalMovedEvent` в **Zoo.Domain/Events/AnimalMovedEvent.cs**
   - **Контракт**: `IAnimalTransferService` в **Zoo.Application/Interfaces/IAnimalTransferService.cs**

4. **Просмотреть расписание кормления**
   - **Сервис**: `FeedingOrganizationService.GetAllSchedules()` (реализация `IFeedingOrganizationService`) в **Zoo.Application/Services/FeedingOrganizationService.cs**
   - **Контроллер**: `FeedingScheduleController.GetAll()` в **Zoo.Presentation/Controllers/FeedingScheduleController.cs**
   - **Repository**: `IFeedingScheduleRepository.GetAll()` и `InMemoryFeedingScheduleRepository` в соответствующих папках

5. **Добавить новое кормление в расписание**
   - **Сервис**: `FeedingOrganizationService.ScheduleFeeding()`
   - **Контракт**: `IFeedingOrganizationService.ScheduleFeeding()`
   - **Контроллер**: `FeedingScheduleController.Schedule()`
   - **Entity**: `FeedingSchedule` в **Zoo.Domain/Entities/FeedingSchedule.cs**

6. **Просмотреть статистику зоопарка (кол‑во животных, свободные вольеры и т.д.)**
   - **Сервис**: `ZooStatisticsService` (реализация `IZooStatisticsService`) в **Zoo.Application/Services/ZooStatisticsService.cs**
   - **Контракт**: `IZooStatisticsService` в **Zoo.Application/Interfaces/IZooStatisticsService.cs**

> **Примечание:** Для статистики пока не реализован отдельный контроллер, но сервис и интерфейс доступны для подключения.

## 2. Применённые концепции DDD и принципы Clean Architecture

### Domain-Driven Design

- **Сущности:**
  - `Animal`, `Enclosure`, `FeedingSchedule` в **Zoo.Domain/Entities** — содержат уникальный `Guid Id` и бизнес‑логику методов (`Feed`, `AddAnimal`, `Reschedule`, `MarkCompleted`).

- **Value Objects:**
  - `Species`, `AnimalName`, `Capacity`, `EnclosureType`, `Gender` в **Zoo.Domain/ValueObjects** — инкапсулируют примитивы и бизнес‑валидаторы, сравниваются по значению.

- **Доменные события:**
  - `AnimalMovedEvent`, `FeedingTimeEvent` в **Zoo.Domain/Events** — моделируют факты предметной области, рассылаются через `IEventDispatcher`.

- **Правила и инварианты:**
  - В методах `Enclosure.AddAnimal` проверка на переполнение (`Capacity.Max`), в `Animal.Feed` — проверка favoriteFood.

- **Репозитории:**
  - Интерфейсы `IAnimalRepository`, `IEnclosureRepository`, `IFeedingScheduleRepository` в **Zoo.Application/Interfaces** — изолируют доступ к данным.

### Clean Architecture

- **Слои и зависимости:**
  
  - **Domain** — ядро, не держит ссылок ни на один внешний слой.
  - **Application** - опирается только на Domain (для моделей) и задаёт интерфейсы.
  - **Infrastructure** — реализует эти интерфейсы и при этом тоже зависит от Domain (чтобы работать с сущностями).
  - **Presentation** (Web API) - инъектит сервисы по интерфейсам из Application; работает с DTO (и в нашем текущем коде чуть-чуть зависит от Domain для Value Objects/Entity в контроллерах, но лучше держать полностью в границах Application–Presentation через DTO).

- **Dependency Inversion Principle:**
  - Контроллеры и сервисы ссылаются на абстракции (`I*`), а конкретная реализация (`InMemory*`, `SimpleEventDispatcher`) передаётся через DI.

- **Single Responsibility Principle:**
  - Каждый класс выполняет только одну задачу: `AnimalTransferService` — только перемещения, `FeedingOrganizationService` — только кормления, `ZooStatisticsService` — только статистика.

- **Interface Segregation Principle:**
  - Интерфейсы небольшие и ориентированы на конкретные сценарии (`IAnimalTransferService`, `IFeedingOrganizationService`, `IZooStatisticsService`).

- **Изоляция бизнес‑логики:**
  - Domain и Application слои полностью изолированы от технических деталей хранения (Infrastructure) и передачи данных (Presentation).

---

