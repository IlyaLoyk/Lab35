using Lab35.BLL.Exceptions;
using Lab35.BLL.Models;

namespace Lab35.Tests;

public class StudentTests
{
    private static Student Create(string country = "Польща", double grade = 95, int course = 1) =>
        new("Коваленко", course, "КВ12345678", grade, country, "AB123456");

    [Fact]
    public void Constructor_ValidData_SetsNormalizedProperties()
    {
        // Arrange & Act
        var s = new Student("  Коваленко ", 2, " КВ12345678 ", 88.5, " Польща ", " ab123456 ");

        // Assert
        Assert.Equal("Коваленко", s.Surname);
        Assert.Equal(2, s.Course);
        Assert.Equal("КВ12345678", s.StudentTicket);
        Assert.Equal(88.5, s.AverageGrade);
        Assert.Equal("Польща", s.Country);
        Assert.Equal("AB123456", s.ForeignPassportNumber);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("Kov4lenko")]
    [InlineData("123")]
    public void Constructor_InvalidSurname_Throws(string? surname)
    {
        // Arrange & Act
        void Act() => new Student(surname, 1, "КВ12345678", 90, "Польща", "AB123456");

        // Assert
        Assert.Throws<ValidationException>(Act);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(7)]
    public void Constructor_InvalidCourse_Throws(int course)
    {
        // Arrange & Act
        void Act() => new Student("Коваленко", course, "КВ12345678", 90, "Польща", "AB123456");

        // Assert
        Assert.Throws<ValidationException>(Act);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("12345678")]
    [InlineData("КВ1234")]
    [InlineData("КВ123456789")]
    public void Constructor_InvalidTicket_Throws(string? ticket)
    {
        // Arrange & Act
        void Act() => new Student("Коваленко", 1, ticket, 90, "Польща", "AB123456");

        // Assert
        Assert.Throws<ValidationException>(Act);
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(100.1)]
    [InlineData(double.NaN)]
    public void Constructor_InvalidGrade_Throws(double grade)
    {
        // Arrange & Act
        void Act() => new Student("Коваленко", 1, "КВ12345678", grade, "Польща", "AB123456");

        // Assert
        Assert.Throws<ValidationException>(Act);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("   ")]
    public void Constructor_InvalidCountry_Throws(string? country)
    {
        // Arrange & Act
        void Act() => new Student("Коваленко", 1, "КВ12345678", 90, country, "AB123456");

        // Assert
        Assert.Throws<ValidationException>(Act);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("AB12")]
    [InlineData("AB 123456")]
    [InlineData("AB1234567890123")]
    public void Constructor_InvalidPassport_Throws(string? passport)
    {
        // Arrange & Act
        void Act() => new Student("Коваленко", 1, "КВ12345678", 90, "Польща", passport);

        // Assert
        Assert.Throws<ValidationException>(Act);
    }

    [Theory]
    [InlineData("Україна", false)]
    [InlineData("україна", false)]
    [InlineData("Ukraine", false)]
    [InlineData("UKRAINE", false)]
    [InlineData("Польща", true)]
    public void IsForeign_DependsOnCountry(string country, bool expected)
    {
        // Arrange
        var student = Create(country: country);

        // Act
        var result = student.IsForeign;

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(90, true)]
    [InlineData(100, true)]
    [InlineData(89.99, false)]
    [InlineData(0, false)]
    public void IsExcellent_DependsOnGrade(double grade, bool expected)
    {
        // Arrange
        var student = Create(grade: grade);

        // Act
        var result = student.IsExcellent;

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(40)]
    public void Dive_ValidDepth_ReturnsMessage(int depth)
    {
        // Arrange
        var student = Create();

        // Act
        var message = student.Dive(depth);

        // Assert
        Assert.Contains("Коваленко", message);
        Assert.Contains($"{depth} м", message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    [InlineData(41)]
    public void Dive_InvalidDepth_Throws(int depth)
    {
        // Arrange
        var student = Create();

        // Act
        void Act() => student.Dive(depth);

        // Assert
        Assert.Throws<ValidationException>(Act);
    }
}
