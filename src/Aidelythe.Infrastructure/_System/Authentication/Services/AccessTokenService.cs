using Aidelythe.Application._System.Authentication.Core;
using Aidelythe.Application._System.Authentication.Projections;
using Aidelythe.Application._System.Authentication.Services;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Infrastructure._System.Authentication.Services;

/// <summary>
/// Represents a service for managing access tokens.
/// </summary>
public sealed class AccessTokenService : IAccessTokenService
{
    /// <inheritdoc/>
    public TokenInfo Issue(UserId userId)
    {
        // TODO: get from config as options
        var issuer = "Aidelythe";
        var audience = "Aidelythe";
        var expiresIn = 30;
        var signingKey = new byte[32];

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(signingKey),
            SecurityAlgorithms.HmacSha256Signature);

        var subject = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.NameIdentifier, $"{userId}"),
            new Claim(ClaimTypes.Role, AppRoles.Member),
        ]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Audience = audience,
            Subject = subject,
            Expires = DateTime.UtcNow.AddMinutes(expiresIn),
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JsonWebTokenHandler
        {
            SetDefaultTimesOnTokenCreation = false
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new TokenInfo(token, expiresIn);
    }
}