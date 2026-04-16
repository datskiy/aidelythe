using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Domain.Tests.Identity.Users.ValueObjects;

public sealed class UserIdTests
{
    [Fact]
    public void ToString_ShouldReturnEncapsulatedValue()
    {
        // Arrange
        var userId = UserId.New();

        // Act
        var userIdString = userId.ToString();

        // Assert
        Assert.Equal(userIdString, userId.Value.ToString());
    }
}