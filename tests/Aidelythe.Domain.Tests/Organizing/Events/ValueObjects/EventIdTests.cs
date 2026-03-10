using Aidelythe.Domain.Organizing.Events.ValueObjects;

namespace Aidelythe.Domain.Tests.Organizing.Events;

public sealed class EventIdTests
{
    [Fact]
    public void ToString_ShouldReturnEncapsulatedValue()
    {
        // Arrange
        var eventId = EventId.New();

        // Act
        var eventIdString = eventId.ToString();

        // Assert
        Assert.Equal(eventIdString, eventId.Value.ToString());
    }
}