using Aidelythe.Application._System.Authentication.Discriminants;
using Aidelythe.Application._System.Authentication.Projections;

namespace Aidelythe.Application._System.Authentication.Results;

/// <summary>
/// Represents the result of refreshing the token pair of a user.
/// </summary>
public sealed class RefreshResult
{
    /// <summary>
    /// Gets the discriminated union containing all possible outcomes.
    /// </summary>
    public OneOf<TokenPairDetails, InvalidToken> Union { get; }

    private RefreshResult(OneOf<TokenPairDetails, InvalidToken> union)
    {
        Union = union;
    }

    public static implicit operator RefreshResult(TokenPairDetails tokenPairDetails)
    {
        return new RefreshResult(tokenPairDetails);
    }

    public static implicit operator RefreshResult(InvalidToken invalidToken)
    {
        return new RefreshResult(invalidToken);
    }
}