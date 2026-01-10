using Aidelythe.Application._System.Authentication.Discriminants;
using Aidelythe.Application._System.Authentication.Projections;

namespace Aidelythe.Application._System.Authentication.Results;

/// <summary>
/// Represents the result of logging in a user.
/// </summary>
public sealed class LoginResult
{
    /// <summary>
    /// Gets the discriminated union containing all possible outcomes.
    /// </summary>
    public OneOf<TokenPairDetails, InvalidCredentials> Union { get; }

    private LoginResult(OneOf<TokenPairDetails, InvalidCredentials> union)
    {
        Union = union;
    }

    public static implicit operator LoginResult(TokenPairDetails tokenPairDetails)
    {
        return new LoginResult(tokenPairDetails);
    }

    public static implicit operator LoginResult(InvalidCredentials invalidCredentials)
    {
        return new LoginResult(invalidCredentials);
    }
}