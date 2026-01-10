using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Shared.Time;

namespace Aidelythe.Application._System.Authentication.Projections;

/// <summary>
/// Represents the details of a token pair.
/// </summary>
public sealed class TokenPairDetails
{
    /// <summary>
    /// Gets the details of the refresh token.
    /// </summary>
    public TokenDetails RefreshToken { get; }

    /// <summary>
    /// Gets the details of the access token.
    /// </summary>
    public TokenDetails AccessToken { get; }

    private TokenPairDetails(
        TokenDetails refreshToken,
        TokenDetails accessToken)
    {
        RefreshToken = refreshToken;
        AccessToken = accessToken;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="TokenPairDetails"/> class.
    /// </summary>
    /// <param name="refreshTokenDescriptor">The refresh token descriptor.</param>
    /// <param name="accessTokenDescriptor">The access token descriptor.</param>
    /// <returns></returns>
    public static TokenPairDetails Create(
        RefreshTokenDescriptor refreshTokenDescriptor,
        AccessTokenDescriptor accessTokenDescriptor)
    {
        var refreshTokenDetails = new TokenDetails(
            refreshTokenDescriptor.Token.Value,
            refreshTokenDescriptor.ExpiresAt.GetSecondsUntilNowUtc());

        var accessTokenDetails = new TokenDetails(
            accessTokenDescriptor.Token.Value,
            accessTokenDescriptor.ExpiresAt.GetSecondsUntilNowUtc());

        return new TokenPairDetails(refreshTokenDetails, accessTokenDetails);
    }
}