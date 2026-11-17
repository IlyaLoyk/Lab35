using Lab35.BLL.Exceptions;

namespace Lab35.BLL.Models;

public class Tailor : Worker
{
    public Tailor(string? surname) : base(surname)
    {
    }

    public string Sew(string? garment)
    {
        if (string.IsNullOrWhiteSpace(garment))
            throw new ValidationException("Не вказано, що шити.");

        return $"Кравець {Surname} пошив: {garment.Trim()}.";
    }
}
