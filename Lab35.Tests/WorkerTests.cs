using Lab35.BLL.Exceptions;
using Lab35.BLL.Models;

namespace Lab35.Tests;

public class WorkerTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("  ")]
    public void Tailor_InvalidSurname_Throws(string? surname)
    {
        // Arrange & Act
        void Act() => new Tailor(surname);

        // Assert
        Assert.Throws<ValidationException>(Act);
    }

    [Fact]
    public void Singer_InvalidSurname_Throws()
    {
        // Arrange & Act
        void Act() => new Singer("");

        // Assert
        Assert.Throws<ValidationException>(Act);
    }

    [Fact]
    public void Tailor_LearnInstrument_NewInstrument_ReturnsTrueAndStores()
    {
        // Arrange
        var tailor = new Tailor(" Іваненко ");

        // Act
        var added = tailor.LearnInstrument(" Гітара ");

        // Assert
        Assert.True(added);
        Assert.Equal("Іваненко", tailor.Surname);
        Assert.Single(tailor.Instruments);
        Assert.Contains("Гітара", tailor.Instruments);
    }

    [Fact]
    public void Singer_LearnInstrument_DuplicateIgnoringCase_ReturnsFalse()
    {
        // Arrange
        var singer = new Singer("Петренко");
        singer.LearnInstrument("Піаніно");

        // Act
        var added = singer.LearnInstrument("піаніно");

        // Assert
        Assert.False(added);
        Assert.Single(singer.Instruments);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void LearnInstrument_BlankName_Throws(string? instrument)
    {
        // Arrange
        var singer = new Singer("Петренко");

        // Act
        void Act() => singer.LearnInstrument(instrument);

        // Assert
        Assert.Throws<ValidationException>(Act);
    }

    [Fact]
    public void Tailor_Sew_ReturnsMessage()
    {
        // Arrange
        var tailor = new Tailor("Іваненко");

        // Act
        var message = tailor.Sew(" пальто ");

        // Assert
        Assert.Equal("Кравець Іваненко пошив: пальто.", message);
    }

    [Fact]
    public void Tailor_Sew_BlankGarment_Throws()
    {
        // Arrange
        var tailor = new Tailor("Іваненко");

        // Act
        void Act() => tailor.Sew(" ");

        // Assert
        Assert.Throws<ValidationException>(Act);
    }

    [Fact]
    public void Singer_Sing_ReturnsMessage()
    {
        // Arrange
        var singer = new Singer("Петренко");

        // Act
        var message = singer.Sing("Червона рута");

        // Assert
        Assert.Equal("Співак Петренко виконав пісню «Червона рута».", message);
    }

    [Fact]
    public void Singer_Sing_BlankSong_Throws()
    {
        // Arrange
        var singer = new Singer("Петренко");

        // Act
        void Act() => singer.Sing(null);

        // Assert
        Assert.Throws<ValidationException>(Act);
    }
}
