using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application.Tests._System.Authentication.Data;

public sealed class AccessTokenDescriptorTests
{
    [Fact]
    public void Ctor_WhenAccessTokenIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var nullAccessToken = (AccessToken?)null;
        var expiresAtUtc = DateTimeOffset.UtcNow.AddSeconds(1);

        // Act
        var tryCreate = () => new AccessTokenDescriptor(
            nullAccessToken!,
            expiresAtUtc);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenArgumentsAreValid_ShouldReturnAccessTokenDescriptor()
    {
        // Arrange
        var accessToken = new AccessToken("access-token");
        var expiresAtUtc = DateTimeOffset.UtcNow.AddSeconds(1);

        // Act
        var accessTokenDescriptor = new AccessTokenDescriptor(
            accessToken,
            expiresAtUtc);

        // Assert
        Assert.NotNull(accessTokenDescriptor);
    }
}