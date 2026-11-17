namespace Lab35.BLL.Abstractions;

/// <summary>
/// "Сирий" запис про студента, який повертає DAL. Дані ще не перевірені —
/// контроль даних виконується в BLL при створенні Student.
/// </summary>
public record StudentRecord(
    string? Surname,
    int Course,
    string? StudentTicket,
    double AverageGrade,
    string? Country,
    string? ForeignPassportNumber);
