using Aidelythe.Domain.Organizing.Events.ValueObjects;

namespace Aidelythe.Domain.Tests.Organizing.Events.ValueObjects;

public sealed class EventDescriptionTests
{
    [Fact]
    public void Ctor_WhenValueIsNullOrWhiteSpace_ShouldThrowArgumentException()
    {
        // Arrange
        var nullValue = (string?)null;
        var emptyValue = string.Empty;
        var whiteSpaceValue = " ";

        // Act
        var tryCreateWithNullValue = () => new EventDescription(nullValue!);
        var tryCreateWithEmptyValue = () => new EventDescription(emptyValue);
        var tryCreateWithWhiteSpaceValue = () => new EventDescription(whiteSpaceValue);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreateWithNullValue);
        Assert.Throws<ArgumentException>(tryCreateWithEmptyValue);
        Assert.Throws<ArgumentException>(tryCreateWithWhiteSpaceValue);
    }

    [Fact]
    public void Ctor_WhenValueIsLongerThanMaximumLength_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var longValue = new string('x', EventDescription.MaximumLength + 1);

        // Act
        var tryCreate = () => new EventDescription(longValue);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenValueIsValid_ShouldReturnEventDescription()
    {
        // Arrange
        var value = "Test Event Description";

        // Act
        var eventDescription = new EventDescription(value);

        // Assert
        Assert.True(eventDescription is not null);
    }

    [Fact]
    public void ToString_ShouldReturnEncapsulatedValue()
    {
        // Arrange
        var eventDescription = new EventDescription("Test Event Description");

        // Act
        var eventDescriptionString = eventDescription.ToString();

        // Assert
        Assert.Equal(eventDescriptionString, eventDescription.Value);
    }
}