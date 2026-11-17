using Lab35.BLL.Exceptions;

namespace Lab35.BLL.Models;

public class Singer : Worker
{
    public Singer(string? surname) : base(surname)
    {
    }

    public string Sing(string? song)
    {
        if (string.IsNullOrWhiteSpace(song))
            throw new ValidationException("Не вказано пісню.");

        return $"Співак {Surname} виконав пісню «{song.Trim()}».";
    }
}
