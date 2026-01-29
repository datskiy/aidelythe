using Aidelythe.Application._System.Authentication.Core;
using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Services;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Infrastructure._System.Authentication.Services;

/// <summary>
/// Represents a service for managing access tokens.
/// </summary>
public sealed class AccessTokenService : IAccessTokenService
{
    /// <inheritdoc/>
    public AccessTokenDescriptor Issue(
        UserId userId,
        UserSessionId userSessionId)
    {
        // TODO: get from config as options
        var issuer = "Aidelythe";
        var audience = "Aidelythe";
        var expiresIn = 20;
        var signingKey = new byte[32];

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(signingKey),
            SecurityAlgorithms.HmacSha256Signature);

        var subject = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.NameIdentifier, $"{userId}"),
            new Claim(ClaimTypes.Role, AppRoles.Member),
            new Claim(ClaimTypes.Sid, $"{userSessionId}")
        ]);

        var expiresAt = DateTime.UtcNow.AddMinutes(expiresIn);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Audience = audience,
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