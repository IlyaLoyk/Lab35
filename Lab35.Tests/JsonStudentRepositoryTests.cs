using Lab35.BLL.Abstractions;
using Lab35.DAL;

namespace Lab35.Tests;

public class JsonStudentRepositoryTests : IDisposable
{
    private readonly string _path = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");

    public void Dispose()
    {
        if (File.Exists(_path))
            File.Delete(_path);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("  ")]
    public void Constructor_BlankPath_Throws(string? path)
    {
        // Arrange & Act
        void Act() => new JsonStudentRepository(path!);

        // Assert
        Assert.ThrowsAny<ArgumentException>(Act);
    }

    [Fact]
    public void LoadAll_FileMissing_Throws()
    {
        // Arrange
        var repo = new JsonStudentRepository(_path);

        // Act
        void Act() => repo.LoadAll();

        // Assert
        Assert.Throws<FileNotFoundException>(Act);
    }

    [Fact]
    public void SaveAll_ThenLoadAll_ReturnsSameRecords()
    {
        // Arrange
        var repo = new JsonStudentRepository(_path);
        var records = new[]
        {
            new StudentRecord("Коваленко", 1, "КВ12345678", 95, "Україна", "FA123456"),
            new StudentRecord("Smith", 2, "КВ12345679", 80.5, "США", "US7654321")
        };

        // Act
        repo.SaveAll(records);
        var loaded = repo.LoadAll();

        // Assert
        Assert.Equal(records, loaded);
    }

    [Fact]
    public void LoadAll_JsonNull_ReturnsEmptyList()
    {
        // Arrange
        File.WriteAllText(_path, "null");
        var repo = new JsonStudentRepository(_path);

        // Act
        var loaded = repo.LoadAll();

        // Assert
        Assert.Empty(loaded);
    }
}
