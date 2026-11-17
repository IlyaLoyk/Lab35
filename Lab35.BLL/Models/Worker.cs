using Lab35.BLL.Exceptions;

namespace Lab35.BLL.Models;

/// <summary>Базовий клас для додаткових сутностей (Tailor, Singer).</summary>
public abstract class Worker : IInstrumentLearner
{
    private readonly HashSet<string> _instruments = new(StringComparer.OrdinalIgnoreCase);

    protected Worker(string? surname)
    {
        if (string.IsNullOrWhiteSpace(surname))
            throw new ValidationException("Прізвище не може бути порожнім.");

        Surname = surname.Trim();
    }

    public string Surname { get; }

    public IReadOnlyCollection<string> Instruments => _instruments;

    public bool LearnInstrument(string? instrument)
    {
        if (string.IsNullOrWhiteSpace(instrument))
            throw new ValidationException("Назва музичного інструмента не може бути порожньою.");

        return _instruments.Add(instrument.Trim());
    }
}
