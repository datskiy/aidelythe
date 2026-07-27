using Aidelythe.Application._System.Authentication.Core;
using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Services;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;
using Aidelythe.Shared.Settings;

namespace Aidelythe.Infrastructure._System.Authentication.Services;

/// <summary>
/// Represents a service for managing access tokens.
/// </summary>
public sealed class AccessTokenService : IAccessTokenService
{
    private readonly TimeProvider _timeProvider;
    private readonly AccessTokenSettings _accessTokenSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccessTokenService"/> class.
    /// </summary>
    /// <param name="timeProvider">The instance of <see cref="TimeProvider"/>.</param>
    /// <param name="accessTokenOptions">The instance of <see cref="IOptions{AccessTokenSettings}"/>.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="timeProvider"/> or <paramref name="accessTokenOptions"/> is null.
    /// </exception>
    public AccessTokenService(
        TimeProvider timeProvider,
        IOptions<AccessTokenSettings> accessTokenOptions)
    {
        ThrowIfNull(timeProvider);
        ThrowIfNull(accessTokenOptions);

        _timeProvider = timeProvider;
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

        var expiresAt = _timeProvider
            .GetUtcNow()
            .AddSeconds(_accessTokenSettings.ExpiresInSeconds);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _accessTokenSettings.Issuer,
            Audience = _accessTokenSettings.Audience,
            Subject = subject,
            Expires = expiresAt.UtcDateTime,
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