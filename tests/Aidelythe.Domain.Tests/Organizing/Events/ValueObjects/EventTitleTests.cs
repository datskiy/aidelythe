using Aidelythe.Domain.Organizing.Events.ValueObjects;

namespace Aidelythe.Domain.Tests.Organizing.Events.ValueObjects;

public sealed class EventTitleTests
{
    [Fact]
    public void Ctor_WhenValueIsNullOrWhiteSpace_ShouldThrowArgumentException()
    {
        // Arrange
        var nullValue = (string?)null;
        var emptyValue = string.Empty;
        var whiteSpaceValue = " ";

        // Act
        var tryCreateWithNullValue = () => new EventTitle(nullValue!);
        var tryCreateWithEmptyValue = () => new EventTitle(emptyValue);
        var tryCreateWithWhiteSpaceValue = () => new EventTitle(whiteSpaceValue);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreateWithNullValue);
        Assert.Throws<ArgumentException>(tryCreateWithEmptyValue);
        Assert.Throws<ArgumentException>(tryCreateWithWhiteSpaceValue);
    }

    [Fact]
    public void Ctor_WhenValueIsLongerThanMaximumLength_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var longValue = new string('x', EventTitle.MaximumLength + 1);

        // Act
        var tryCreate = () => new EventTitle(longValue);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenValueIsValid_ShouldReturnEventTitle()
    {
        // Arrange
        var value = "Test Event Title";

        // Act
        var eventTitle = new EventTitle(value);

        // Assert
        Assert.True(eventTitle is not null);
    }

    [Fact]
    public void ToString_ShouldReturnEncapsulatedValue()
    {
        // Arrange
        var eventTitle = new EventTitle("Test Event Title");

        // Act
        var eventTitleString = eventTitle.ToString();

        // Assert
        Assert.Equal(eventTitleString, eventTitle.Value);
    }
}