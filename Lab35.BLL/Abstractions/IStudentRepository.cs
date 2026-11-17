namespace Lab35.BLL.Abstractions;

/// <summary>
/// Абстракція доступу до даних (DAL). BLL залежить лише від цього інтерфейсу,
/// тому в тестах його можна підмінити заглушкою (mock/stub) — Dependency Injection.
/// </summary>
public interface IStudentRepository
{
    IReadOnlyList<StudentRecord> LoadAll();
}
