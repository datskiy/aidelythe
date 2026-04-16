using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Infrastructure._System.Authentication.Services;

namespace Aidelythe.Infrastructure.Tests._System.Authentication.Services;

public sealed class PasswordServiceTests
{
    [Fact]
    public void Hash_WhenPasswordIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sut = CreateSut();
        var nullPassword = (Password?)null;

        // Act
        var tryHash = () => sut.Hash(nullPassword!);

        // Assert
        Assert.Throws<ArgumentNullException>(tryHash);
    }

    [Fact]
    public void Hash_WhenPasswordIsValid_ShouldReturnPasswordHash()
    {
        // Arrange
        var sut = CreateSut();
        var password = new Password("admin694201337");

        // Act
        var passwordHash = sut.Hash(password);

        // Assert
        Assert.NotNull(passwordHash);
        Assert.NotEqual(password.Value, passwordHash.Value);
    }

    [Fact]
    public void Verify_WhenPasswordOrPasswordHashIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sut = CreateSut();
        var password = new Password("admin694201337");
        var passwordHash = new PasswordHash("hashed-password");
        var nullPassword = (Password?)null;
        var nullPasswordHash = (PasswordHash?)null;

        // Act
        var tryVerifyWithNullPassword = () => sut.Verify(nullPassword!, passwordHash).Value;
        var tryVerifyWithNullPasswordHash = () => sut.Verify(password, nullPasswordHash!).Value;

        // Assert
        Assert.Throws<ArgumentNullException>(tryVerifyWithNullPassword);
        Assert.Throws<ArgumentNullException>(tryVerifyWithNullPasswordHash);
    }

    [Fact]
    public void Verify_WhenPasswordDoesNotMatchPasswordHash_ShouldReturnFailure()
    {
        // Arrange
        var sut = CreateSut();
        var password = new Password("admin694201337");
        var notMatchingPasswordHash = new PasswordHash("TWFueSBoYW5kcyBtYWtlIGxpZ2h0IHdvcmsu");

        // Act
        var result = sut.Verify(password, notMatchingPasswordHash);

        // Assert
        Assert.IsType<Failure>(result.Value);
    }

    [Fact]
    public void Verify_WhenPasswordMatchesPasswordHash_ShouldReturnSuccess()
    {
        // Arrange
        var sut = CreateSut();
        var password = new Password("admin694201337");
        var matchingPasswordHash = new PasswordHash(
            "AQAAAAIAAYagAAAAEJ0CYNKg66NBAwQ//mp1pCdB+t1UMX6dj9C/4pl7gDSgivnfHHgcOO15PK6jcN0fUg==");

        // Act
        var result = sut.Verify(password, matchingPasswordHash);

        // Assert
        Assert.IsType<Success>(result.Value);
    }

    private PasswordService CreateSut()
    {
        return new PasswordService();
    }
}