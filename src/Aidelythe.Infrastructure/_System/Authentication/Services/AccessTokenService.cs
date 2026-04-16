using Aidelythe.Application._System.Authentication.Core;
using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Services;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;
using Aidelythe.Infrastructure._Common.Settings;

namespace Aidelythe.Infrastructure._System.Authentication.Services;

/// <summary>
/// Represents a service for managing access tokens.
/// </summary>
public sealed class AccessTokenService : IAccessTokenService
{
    private readonly AccessTokenSettings _accessTokenSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccessTokenService"/> class.
    /// </summary>
    /// <param name="accessTokenOptions">The instance of <see cref="IOptions{AccessTokenSettings}"/>.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="accessTokenOptions"/> is null.</exception>
    public AccessTokenService(IOptions<AccessTokenSettings> accessTokenOptions)
    {
        ThrowIfNull(accessTokenOptions);

        _accessTokenSettings = accessTokenOptions.Value;
    }

    /// <inheritdoc/>
    public AccessTokenDescriptor Issue(
        UserId userId,
        UserSessionId userSessionId)
    {
        var signingKey = Convert.FromBase64String(_accessTokenSettings.SigningKey);

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(signingKey),
            SecurityAlgorithms.HmacSha256Signature);

        var subject = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.NameIdentifier, $"{userId}"),
            new Claim(ClaimTypes.Role, AppRoles.Member),
            new Claim(ClaimTypes.Sid, $"{userSessionId}")
        ]);

        var expiresAt = DateTime.UtcNow.AddSeconds(_accessTokenSettings.ExpiresInSeconds);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _accessTokenSettings.Issuer,
            Audience = _accessTokenSettings.Audience,
            Subject = subject,
            Expires = expiresAt,
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JsonWebTokenHandler
        {
            SetDefaultTimesOnTokenCreation = false
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = new AccessToken(token);

        return new AccessTokenDescriptor(accessToken, expiresAt);
    }
}