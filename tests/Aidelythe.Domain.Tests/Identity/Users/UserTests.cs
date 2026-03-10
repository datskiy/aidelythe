using Aidelythe.Domain.Identity.Users;

namespace Aidelythe.Domain.Tests.Identity.Users;

public sealed class UserTests
{
    [Fact]
    public void Register_ShouldReturnUser()
    {
        // Act
        var user = User.Register();

        // Assert
        Assert.True(user is not null);
    }
}