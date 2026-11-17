using Lab35.BLL.Abstractions;
using Lab35.BLL.Exceptions;
using Lab35.BLL.Models;

namespace Lab35.BLL.Services;

/// <summary>Операції зі студентами (варіант 12).</summary>
public class StudentService
{
    public const int CitizenshipRequiredYears = 5;

    private readonly IStudentRepository _repository;

    // Dependency Injection: залежність передається ззовні
    public StudentService(IStudentRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <summary>Отримати студентів з джерела даних (файл) з перевіркою кожного запису.</summary>
    public IReadOnlyList<Student> GetAllStudents() =>
        _repository.LoadAll()
            .Select(r => new Student(r.Surname, r.Course, r.StudentTicket, r.AverageGrade, r.Country, r.ForeignPassportNumber))
            .ToList();

    /// <summary>Кількість іноземних студентів-відмінників 1-го курсу (дані з файлу).</summary>
    public int CountForeignExcellentFirstYear() =>
        CountForeignExcellentFirstYear(GetAllStudents());

    public int CountForeignExcellentFirstYear(IEnumerable<Student> students)
    {
        ArgumentNullException.ThrowIfNull(students);

        return students.Count(s => s.Course == 1 && s.IsForeign && s.IsExcellent);
    }

    /// <summary>
    /// Можливість отримання українського громадянства:
    /// студент — іноземець і проживає в Україні 5+ років.
    /// </summary>
    public bool CanObtainUkrainianCitizenship(Student student, int yearsInUkraine)
    {
        ArgumentNullException.ThrowIfNull(student);

        if (yearsInUkraine < 0)
            throw new ValidationException("Кількість років проживання не може бути від'ємною.");

        return student.IsForeign && yearsInUkraine >= CitizenshipRequiredYears;
    }
}
