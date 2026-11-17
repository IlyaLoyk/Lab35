# Лабораторна робота 3.5 — Модульне тестування (варіант 12)

Student: Прізвище, Курс, Студентський квиток, Середній бал, Країна, № закордонного паспорта.

| Рівень оцінки  | Що реалізовано                                                                              |
| -------------- | ------------------------------------------------------------------------------------------- |
| Задовільно     | `Student` + `StudentService.CountForeignExcellentFirstYear` (дані з файлу)                  |
| Добре          | Вміння «Пірнати» (`IDiver`, `Student.Dive`), громадянство (`CanObtainUkrainianCitizenship`) |
| Відмінно       | `Tailor`, `Singer` (`Worker`) з операцією «навчатися грі на музичному інструменті»          |
| Додаткове (\*) | DI: `StudentService` отримує `IStudentRepository`, у тестах — Moq                           |

Шари: `Lab35.PL` (Console) → `Lab35.BLL` (логіка + валідація) ← `Lab35.DAL` (JSON-файл).

## Запуск

```
dotnet build
dotnet test
dotnet run --project Lab35.PL
```

## Покриття (Code Coverage)

```
dotnet test --collect:"XPlat Code Coverage"
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:"Lab35.Tests/TestResults/*/coverage.cobertura.xml" -targetdir:coverage -reporttypes:Html
```

start coverage\index.html

## Прийняті припущення

- Шкала 100-бальна, відмінник — середній бал ≥ 90.
- Іноземець — країна не «Україна»/«Ukraine».
- Кількість років проживання в Україні передається параметром операції (у списку полів студента його немає).
- Формат студентського квитка: 2 літери + 8 цифр; паспорт: 6–12 латинських літер/цифр.
