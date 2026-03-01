using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application.Tests._System.Authentication.ValueObjects;

public sealed class UserSessionIdTests
{
    [Fact]
    public void ToString_ShouldReturnEncapsulatedValue()
    {
        // Arrange
        var userSessionId = UserSessionId.New();

        // Act
        var userSessionIdString = userSessionId.ToString();

        // Assert
        Assert.Equal(userSessionIdString, userSessionId.Value.ToString());
    }
}