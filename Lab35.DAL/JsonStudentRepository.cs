using System.Text.Encodings.Web;
using System.Text.Json;
using Lab35.BLL.Abstractions;

namespace Lab35.DAL;

/// <summary>Доступ до даних через JSON-файл (серіалізація).</summary>
public class JsonStudentRepository : IStudentRepository
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private readonly string _path;

    public JsonStudentRepository(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        _path = path;
    }

    public IReadOnlyList<StudentRecord> LoadAll()
    {
        if (!File.Exists(_path))
            throw new FileNotFoundException("Файл з даними студентів не знайдено.", _path);

        var json = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<List<StudentRecord>>(json, Options) ?? new List<StudentRecord>();
    }

    public void SaveAll(IEnumerable<StudentRecord> records)
    {
        File.WriteAllText(_path, JsonSerializer.Serialize(records, Options));
    }
}
