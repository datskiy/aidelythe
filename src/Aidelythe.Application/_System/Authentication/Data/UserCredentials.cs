using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Data;

/// <summary>
/// Represents user credentials.
/// </summary>
public sealed class UserCredentials
{
    /// <summary>
    /// Gets the unique identifier of the user.
    /// </summary>
    public UserId UserId { get; }

    /// <summary>
    /// Gets the email address of the user.
    /// </summary>
    public Email? Email { get; }

    /// <summary>
    /// Gets the phone number of the user.
    /// </summary>
    public PhoneNumber? PhoneNumber { get; }

    /// <summary>
    /// Gets the hashed password of the user.
    /// </summary>
    public PasswordHash PasswordHash { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserCredentials"/> class.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <param name="phoneNumber">The phone number of the user.</param>
    /// <param name="passwordHash">The hashed password of the user.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="passwordHash"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Both <paramref name="email"/> and <paramref name="phoneNumber"/> are null.
    /// </exception>
    public UserCredentials(
        UserId userId,
        Email? email,
        PhoneNumber? phoneNumber,
        PasswordHash passwordHash)
    {
        ThrowIfNull(passwordHash);

        if (email is null && phoneNumber is null)
            throw new ArgumentException("At least one contact method must be provided.");

        UserId = userId;
        Email = email;
        PhoneNumber = phoneNumber;
        PasswordHash = passwordHash;
    }

    /// <summary>
    /// Updates the hashed password of the user.
    /// </summary>
    /// <param name="passwordHash">The new hashed password of the user.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="passwordHash"/> is null.</exception>
    public void UpdatePasswordHash(PasswordHash passwordHash)
    {
        ThrowIfNull(passwordHash);

        PasswordHash = passwordHash;
    }
}