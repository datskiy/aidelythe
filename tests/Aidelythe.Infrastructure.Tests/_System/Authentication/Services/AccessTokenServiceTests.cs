using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;
using Aidelythe.Infrastructure._Common.Settings;
using Aidelythe.Infrastructure._System.Authentication.Services;

namespace Aidelythe.Infrastructure.Tests._System.Authentication.Services;

public sealed class AccessTokenServiceTests
{
    [Fact]
    public void Issue_WhenArgumentsAreValid_ShouldReturnAccessTokenDescriptor()
    {
        // Arrange
        var sut = CreateSut();
        var userId = UserId.New();
        var userSessionId = UserSessionId.New();

        // Act
        var accessTokenDescriptor = sut.Issue(userId, userSessionId);

        // Assert
        Assert.True(accessTokenDescriptor is not null);
    }

    private AccessTokenService CreateSut()
    {
        var settings = new AccessTokenSettings
        {
            Issuer = "test-issuer",
            Audience = "test-audience",
            SigningKey = "TWFueSBoYW5kcyBtYWtlIGxpZ2h0IHdvcmsuZ2h0IHdvcmsu",
            ExpiresInSeconds = 1,
            ClockSkewInSeconds = 1
        };

        return new AccessTokenService(
            Options.Create(settings));
    }
}