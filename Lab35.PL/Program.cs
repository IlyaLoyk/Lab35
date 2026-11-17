using System.Text;
using Lab35.BLL.Exceptions;
using Lab35.BLL.Models;
using Lab35.BLL.Services;
using Lab35.DAL;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

var path = args.Length > 0 ? args[0] : Path.Combine(AppContext.BaseDirectory, "students.json");
var service = new StudentService(new JsonStudentRepository(path));

while (true)
{
    Console.WriteLine();
    Console.WriteLine("1 - Показати студентів з файлу");
    Console.WriteLine("2 - Кількість іноземних відмінників 1-го курсу");
    Console.WriteLine("3 - Чи може студент отримати українське громадянство");
    Console.WriteLine("4 - Студент пірнає");
    Console.WriteLine("5 - Tailor та Singer вчаться грати на інструменті");
    Console.WriteLine("0 - Вихід");
    Console.Write("> ");

    try
    {
        switch (Console.ReadLine())
        {
            case "1": ShowStudents(); break;
            case "2": Console.WriteLine($"Кількість: {service.CountForeignExcellentFirstYear()}"); break;
            case "3": CheckCitizenship(); break;
            case "4": DiveDemo(); break;
            case "5": WorkersDemo(); break;
            case "0": return;
            default: Console.WriteLine("Невідомий пункт меню."); break;
        }
    }
    catch (ValidationException ex)
    {
        Console.WriteLine($"Помилка даних: {ex.Message}");
    }
    catch (IOException ex)
    {
        Console.WriteLine($"Помилка доступу до файлу: {ex.Message}");
    }
}

void ShowStudents()
{
    foreach (var s in service.GetAllStudents())
        Console.WriteLine($"{s.Surname,-12} курс {s.Course}, квиток {s.StudentTicket}, бал {s.AverageGrade}, {s.Country}, паспорт {s.ForeignPassportNumber}");
}

Student? FindStudent()
{
    Console.Write("Прізвище студента: ");
    var surname = Console.ReadLine()?.Trim();
    var student = service.GetAllStudents()
        .FirstOrDefault(s => s.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase));
    if (student is null)
        Console.WriteLine("Студента не знайдено.");
    return student;
}

int ReadInt(string prompt)
{
    Console.Write(prompt);
    if (!int.TryParse(Console.ReadLine(), out var value))
        throw new ValidationException("Потрібно ввести ціле число.");
    return value;
}

void CheckCitizenship()
{
    var student = FindStudent();
    if (student is null) return;

    var years = ReadInt("Років проживання в Україні: ");
    var can = service.CanObtainUkrainianCitizenship(student, years);
    Console.WriteLine(can ? "Може отримати громадянство." : "Не може отримати громадянство.");
}

void DiveDemo()
{
    var student = FindStudent();
    if (student is null) return;

    Console.WriteLine(student.Dive(ReadInt("Глибина, м: ")));
}

void WorkersDemo()
{
    var tailor = new Tailor("Іваненко");
    var singer = new Singer("Петренко");

    tailor.LearnInstrument("Гітара");
    singer.LearnInstrument("Піаніно");

    Console.WriteLine($"{tailor.Surname} вчиться: {string.Join(", ", tailor.Instruments)}");
    Console.WriteLine($"{singer.Surname} вчиться: {string.Join(", ", singer.Instruments)}");
    Console.WriteLine(tailor.Sew("пальто"));
    Console.WriteLine(singer.Sing("Червона рута"));
}
