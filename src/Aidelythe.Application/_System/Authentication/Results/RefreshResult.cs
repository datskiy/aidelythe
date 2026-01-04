using Aidelythe.Application._System.Authentication.Discriminants;
using Aidelythe.Application._System.Authentication.Projections;

namespace Aidelythe.Application._System.Authentication.Results;

/// <summary>
/// Represents the result of refreshing the token pair of a user.
/// </summary>
public sealed class RefreshResult
{
    /// <summary>
    /// Gets the discriminated union containing all possible outcomes when creating an event.
    /// </summary>
    public OneOf<TokenPair, InvalidToken> Union { get; }

    private RefreshResult(OneOf<TokenPair, InvalidToken> union)
    {
        Union = union;
    }

    public static implicit operator RefreshResult(TokenPair tokenPair)
    {
        return new RefreshResult(tokenPair);
    }

    public static implicit operator RefreshResult(InvalidToken invalidToken)
    {
        return new RefreshResult(invalidToken);
    }
}