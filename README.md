# CoursePlatformAPI

REST API для платформы онлайн-курсов на ASP.NET Core 8 и EF Core 8. Учебный/демонстрационный проект — курсы, авторы, студенты и запись на курсы.

## Стек

- **ASP.NET Core 8** — Web API + Swagger
- **Entity Framework Core 8** — доступ к данным, миграции
- **SQL Server LocalDB** — база данных
- **Swashbuckle (Swagger)** — документация и тестирование API из коробки

## Архитектура

```
Controllers/   → HTTP-эндпоинты (Courses, Students, Author)
Models/        → сущности EF, DbContext, Fluent API конфигурация, репозитории
Migrations/    → миграции EF Core
```

Слой доступа к данным построен по паттерну **Repository**: каждый контроллер получает через DI свой репозиторий (`CourseRepository`, `StudentRepository`, `AuthRepository`), который инкапсулирует запросы к `Context : DbContext`.

## Модель данных

- **AuthorEntity** — автор курса
- **EntityCourse** — курс (`Title`, `Description`, `Price`)
- **LessonEntity** — урок курса
- **StudentEntity** — студент

Связи:

| Связь | Тип |
|---|---|
| Author ↔ Course | один-к-одному |
| Course ↔ Lessons | один-ко-многим |
| Course ↔ Students | многие-ко-многим |

Настроены через Fluent API (`AuthorConfiguration`, `CourseConfiguration` в `Models/Configure.cs`).

## Эндпоинты

### Courses (`/api/courses`)
| Метод | Путь | Описание |
|---|---|---|
| GET | `/` | Все курсы (с автором и уроками) |
| GET | `/{id}` | Курс по id |
| GET | `/search?title=` | Поиск по названию |
| GET | `/overthenminprise?price=` | Курсы дороже указанной цены |
| GET | `/count` | Количество курсов |
| POST | `/` | Создать курс |
| PUT | `/{id}` | Обновить курс |
| DELETE | `/{id}` | Удалить курс |

### Students (`/api/students`)
| Метод | Путь | Описание |
|---|---|---|
| GET | `/` | Все студенты с курсами |
| GET | `/{id}` | Студент по id |
| POST | `/` | Создать студента |
| PUT | `/{id}` | Обновить студента |
| DELETE | `/{id}` | Удалить студента |
| POST | `/{studentId}/enroll/{courseId}` | Записать студента на курс |

### Author (`/api/author`)
| Метод | Путь | Описание |
|---|---|---|
| GET | `/` | Все авторы |
| GET | `/{id}` | Автор по id |
| POST | `/` | Создать автора |
| PUT | `/{id}` | Обновить автора |
| DELETE | `/{id}` | Удалить автора |

## Запуск

1. Убедиться, что установлен SQL Server LocalDB (входит в Visual Studio).
2. Применить миграции:

```bash
dotnet ef database update
```

3. Запустить проект:

```bash
dotnet run
```

4. Открыть Swagger UI: `https://localhost:<port>/swagger`

Строка подключения — в `appsettings.json` (`ConnectionStrings:Default`).

## Известные ограничения

- Нет интерфейсов для репозиториев (планируется `ICourseRepository`, `IAuthorRepository`, `IStudentRepository`)
- Нет `CancellationToken` в методах репозиториев/контроллеров
- Нет авторизации (JWT не реализован)
- Нет тестов (планируются xUnit)
- Опечатки в модели: `lessonEntities` → `Lessons`, `CouresID` → `CourseId`
