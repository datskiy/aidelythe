namespace Aidelythe.Application._System.Authentication.Core;

/// <summary>
/// Specifies password verification results.
/// </summary>
public enum PasswordVerificationResult
{
    /// <summary>
    /// Indicates that the password verification has failed.
    /// </summary>
    Failure = 1,

    /// <summary>
    /// Indicates that the password verification has succeeded.
    /// </summary>
    Success = 2,

    /// <summary>
    /// Indicates that the password verification has succeeded, but a rehash of the password is required.
    /// </summary>
    SuccessRehashNeeded = 3
}