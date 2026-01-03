using Aidelythe.Application._System.Authentication.Discriminants;
using Aidelythe.Application._System.Authentication.Projections;

namespace Aidelythe.Application._System.Authentication.Results;

/// <summary>
/// Represents the result of logging in a user.
/// </summary>
public sealed class LoginResult
{
    /// <summary>
    /// Gets the discriminated union containing all possible outcomes when creating an event.
    /// </summary>
    public OneOf<TokenPair, InvalidCredentials> Union { get; }

    private LoginResult(OneOf<TokenPair, InvalidCredentials> union)
    {
        Union = union;
    }

    public static implicit operator LoginResult(TokenPair tokenPair)
    {
        return new LoginResult(tokenPair);
    }

    public static implicit operator LoginResult(InvalidCredentials missingContactMethod)
    {
        return new LoginResult(missingContactMethod);
    }
}