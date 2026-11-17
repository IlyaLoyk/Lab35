using Lab35.BLL.Abstractions;
using Lab35.BLL.Exceptions;
using Lab35.BLL.Models;
using Lab35.BLL.Services;
using Moq;

namespace Lab35.Tests;

public class StudentServiceTests
{
    private static StudentRecord Rec(string surname, int course, double grade, string country) =>
        new(surname, course, "КВ12345678", grade, country, "AB123456");

    private static Mock<IStudentRepository> RepoReturning(params StudentRecord[] records)
    {
        var mock = new Mock<IStudentRepository>();
        mock.Setup(r => r.LoadAll()).Returns(records);
        return mock;
    }

    [Fact]
    public void Constructor_NullRepository_Throws()
    {
        // Arrange & Act
        void Act() => new StudentService(null!);

        // Assert
        Assert.Throws<ArgumentNullException>(Act);
    }

    [Fact]
    public void GetAllStudents_MapsRecordsToStudents()
    {
        // Arrange
        var repo = RepoReturning(Rec("Smith", 1, 97, "США"), Rec("Шевченко", 2, 60, "Україна"));
        var service = new StudentService(repo.Object);

        // Act
        var students = service.GetAllStudents();

        // Assert
        Assert.Equal(2, students.Count);
        Assert.Equal("Smith", students[0].Surname);
        Assert.Equal(2, students[1].Course);
    }

    [Fact]
    public void GetAllStudents_InvalidRecord_ThrowsValidationException()
    {
        // Arrange
        var repo = RepoReturning(Rec("Smith", 9, 97, "США"));
        var service = new StudentService(repo.Object);

        // Act
        void Act() => service.GetAllStudents();

        // Assert
        Assert.Throws<ValidationException>(Act);
    }

    [Fact]
    public void CountForeignExcellentFirstYear_FromRepository_CountsOnlyMatching()
    {
        // Arrange
        var repo = RepoReturning(
            Rec("Smith", 1, 97, "США"),          // підходить
            Rec("Kowalski", 1, 90, "Польща"),    // підходить (межа 90)
            Rec("Hassan", 1, 78, "Єгипет"),      // не відмінник
            Rec("Müller", 2, 93, "Німеччина"),   // не 1-й курс
            Rec("Коваленко", 1, 95, "Україна"));  // не іноземець
        var service = new StudentService(repo.Object);

        // Act
        var count = service.CountForeignExcellentFirstYear();

        // Assert
        Assert.Equal(2, count);
        repo.Verify(r => r.LoadAll(), Times.Once);
    }

    [Fact]
    public void CountForeignExcellentFirstYear_EmptyList_ReturnsZero()
    {
        // Arrange
        var service = new StudentService(RepoReturning().Object);

        // Act
        var count = service.CountForeignExcellentFirstYear(Array.Empty<Student>());

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void CountForeignExcellentFirstYear_NullCollection_Throws()
    {
        // Arrange
        var service = new StudentService(RepoReturning().Object);

        // Act
        void Act() => service.CountForeignExcellentFirstYear(null!);

        // Assert
        Assert.Throws<ArgumentNullException>(Act);
    }

    [Theory]
    [InlineData("Польща", 5, true)]
    [InlineData("Польща", 10, true)]
    [InlineData("Польща", 4, false)]
    [InlineData("Польща", 0, false)]
    [InlineData("Україна", 10, false)]
    public void CanObtainUkrainianCitizenship_DependsOnCountryAndYears(string country, int years, bool expected)
    {
        // Arrange
        var service = new StudentService(RepoReturning().Object);
        var student = new Student("Kowalski", 1, "КВ12345678", 90, country, "AB123456");

        // Act
        var result = service.CanObtainUkrainianCitizenship(student, years);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CanObtainUkrainianCitizenship_NullStudent_Throws()
    {
        // Arrange
        var service = new StudentService(RepoReturning().Object);

        // Act
        void Act() => service.CanObtainUkrainianCitizenship(null!, 5);

        // Assert
        Assert.Throws<ArgumentNullException>(Act);
    }

    [Fact]
    public void CanObtainUkrainianCitizenship_NegativeYears_Throws()
    {
        // Arrange
        var service = new StudentService(RepoReturning().Object);
        var student = new Student("Kowalski", 1, "КВ12345678", 90, "Польща", "AB123456");

        // Act
        void Act() => service.CanObtainUkrainianCitizenship(student, -1);

        // Assert
        Assert.Throws<ValidationException>(Act);
    }
}
