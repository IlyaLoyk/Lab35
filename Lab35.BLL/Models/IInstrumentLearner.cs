namespace Lab35.BLL.Models;

/// <summary>Операція "Навчатися грі на визначеному музичному інструменті".</summary>
public interface IInstrumentLearner
{
    IReadOnlyCollection<string> Instruments { get; }

    /// <returns>true — інструмент новий; false — уже вивчався раніше.</returns>
    bool LearnInstrument(string? instrument);
}
