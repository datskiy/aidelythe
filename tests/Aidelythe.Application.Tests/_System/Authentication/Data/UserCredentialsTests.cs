using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Application.Tests._System.Authentication.Data;

public sealed class UserCredentialsTests
{
    [Fact]
    public void Ctor_WhenPasswordHashIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var userId = UserId.New();
        var email = new Email("user@example.com");
        var nullPasswordHash = (PasswordHash?)null;

        // Act
        var tryCreate = () => new UserCredentials(
            userId,
            email,
            phoneNumber: null,
            nullPasswordHash!);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenBothEmailAndPhoneNumberAreNull_ShouldThrowArgumentException()
    {
        // Arrange
        var userId = UserId.New();
        var nullEmail = (Email?)null;
        var nullPhoneNumber = (PhoneNumber?)null;
        var passwordHash = new PasswordHash("hashed-password");

        // Act
        var tryCreate = () => new UserCredentials(
            userId,
            nullEmail,
            nullPhoneNumber,
            passwordHash);

        // Assert
        Assert.Throws<ArgumentException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenArgumentsAreValid_ShouldReturnUserCredentials()
    {
        // Arrange
        var userId = UserId.New();
        var email = new Email("user@example.com");
        var passwordHash = new PasswordHash("hashed-password");

        // Act
        var userCredentials = new UserCredentials(
            userId,
            email,
            phoneNumber: null,
            passwordHash);

        // Assert
        Assert.True(userCredentials is not null);
    }

    [Fact]
    public void UpdatePasswordHash_WhenPasswordHashIsValid_ShouldUpdatePasswordHash()
    {
        // Arrange
        var userCredentials = new UserCredentials(
            UserId.New(),
            new Email("user@example.com"),
            phoneNumber: null,
            new PasswordHash("hashed-password"));

        var updatedPasswordHash = new PasswordHash("updated-hashed-password");

        // Act
        userCredentials.UpdatePasswordHash(updatedPasswordHash);

        // Assert
        Assert.Equal(updatedPasswordHash, userCredentials.PasswordHash);
    }
}