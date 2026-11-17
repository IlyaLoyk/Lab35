namespace Lab35.BLL.Exceptions;

/// <summary>Помилка валідації даних на рівні бізнес-логіки.</summary>
public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
    }
}
