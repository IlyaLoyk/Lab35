using System.Text.RegularExpressions;
using Lab35.BLL.Exceptions;

namespace Lab35.BLL.Models;

/// <summary>
/// Студент (варіант 12): прізвище, курс, студентський квиток, середній бал,
/// країна, номер закордонного паспорта. Уміє пірнати (IDiver).
/// Контроль даних виконується в конструкторі.
/// </summary>
public class Student : IDiver
{
    public const int MinCourse = 1;
    public const int MaxCourse = 6;
    public const double MaxGrade = 100;               // 100-бальна шкала
    public const double ExcellentGradeThreshold = 90; // відмінник: 90..100
    public const int MaxDiveDepthMeters = 40;
    public const string HomeCountry = "Україна";

    private static readonly Regex SurnameRegex = new(@"^\p{L}[\p{L}'’\- ]*$", RegexOptions.Compiled);
    private static readonly Regex TicketRegex = new(@"^\p{L}{2}[0-9]{8}$", RegexOptions.Compiled);   // напр. КВ12345678
    private static readonly Regex PassportRegex = new(@"^[A-Za-z0-9]{6,12}$", RegexOptions.Compiled); // напр. FA123456

    public Student(
        string? surname,
        int course,
        string? studentTicket,
        double averageGrade,
        string? country,
        string? foreignPassportNumber)
    {
        if (string.IsNullOrWhiteSpace(surname) || !SurnameRegex.IsMatch(surname.Trim()))
            throw new ValidationException("Прізвище має містити лише літери, апостроф, дефіс або пробіл.");

        if (course < MinCourse || course > MaxCourse)
            throw new ValidationException($"Курс має бути від {MinCourse} до {MaxCourse}.");

        if (string.IsNullOrWhiteSpace(studentTicket) || !TicketRegex.IsMatch(studentTicket.Trim()))
            throw new ValidationException("Студентський квиток: 2 літери та 8 цифр (напр. КВ12345678).");

        if (double.IsNaN(averageGrade) || averageGrade < 0 || averageGrade > MaxGrade)
            throw new ValidationException($"Середній бал має бути від 0 до {MaxGrade}.");

        if (string.IsNullOrWhiteSpace(country))
            throw new ValidationException("Країна не може бути порожньою.");

        if (string.IsNullOrWhiteSpace(foreignPassportNumber) || !PassportRegex.IsMatch(foreignPassportNumber.Trim()))
            throw new ValidationException("Номер закордонного паспорта: 6–12 латинських літер/цифр.");

        Surname = surname.Trim();
        Course = course;
        StudentTicket = studentTicket.Trim();
        AverageGrade = averageGrade;
        Country = country.Trim();
        ForeignPassportNumber = foreignPassportNumber.Trim().ToUpperInvariant();
    }

    public string Surname { get; }
    public int Course { get; }
    public string StudentTicket { get; }
    public double AverageGrade { get; }
    public string Country { get; }
    public string ForeignPassportNumber { get; }

    /// <summary>Студент іноземець, якщо країна — не Україна.</summary>
    public bool IsForeign =>
        !(Country.Equals(HomeCountry, StringComparison.OrdinalIgnoreCase) ||
          Country.Equals("Ukraine", StringComparison.OrdinalIgnoreCase));

    public bool IsExcellent => AverageGrade >= ExcellentGradeThreshold;

    public string Dive(int depthMeters)
    {
        if (depthMeters < 1 || depthMeters > MaxDiveDepthMeters)
            throw new ValidationException($"Глибина занурення має бути від 1 до {MaxDiveDepthMeters} м.");

        return $"Студент {Surname} пірнув на глибину {depthMeters} м.";
    }
}
