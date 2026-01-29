using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.Services;
using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Infrastructure._System.Authentication.Services;

/// <summary>
/// Represents a service for managing refresh tokens.
/// </summary>
public sealed class RefreshTokenService : IRefreshTokenService
{
    private readonly IUserSessionRepository _userSessionRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenService"/> class.
    /// </summary>
    /// <param name="userSessionRepository">The instance of <see cref="IUserSessionRepository"/>.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="userSessionRepository"/> is null.</exception>
    public RefreshTokenService(IUserSessionRepository userSessionRepository)
    {
        ThrowIfNull(userSessionRepository);

        _userSessionRepository = userSessionRepository;
    }

    /// <inheritdoc/>
    public RefreshTokenDescriptor Generate()
    {
        // TODO: get from config as options
        var byteCount = 64;
        var expiresIn = 1209600;

        var tokenBytes = RandomNumberGenerator.GetBytes(byteCount);
        var token = Convert.ToBase64String(tokenBytes);
        var refreshToken = new RefreshToken(token);

        var refreshTokenHash = HashToken(tokenBytes);
        var expiresAt = DateTime.UtcNow.AddSeconds(expiresIn);

        return new RefreshTokenDescriptor(
            refreshToken,
            refreshTokenHash,
            expiresAt);
    }

    /// <inheritdoc/>
    public async Task<OneOf<UserSession, Expired, NotFound>> ValidateAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken)
    {
        var tokenBytes = TryGetBytesFromBase64(refreshToken.Value);
        if (tokenBytes is null)
            return new NotFound();

        var refreshTokenHash = HashToken(tokenBytes);

        var userSession = await _userSessionRepository.GetAsync(refreshTokenHash, cancellationToken);
        if (userSession is null)
            return new NotFound();

        if (userSession.IsTokenExpired())
            return new Expired();

        return userSession;
    }

    private static RefreshTokenHash HashToken(byte[] tokenBytes)
    {
        var hashBytes = SHA256.HashData(tokenBytes);
        var hash = Convert.ToBase64String(hashBytes);

        return new RefreshTokenHash(hash);
    }

    private static byte[]? TryGetBytesFromBase64(string base64)
    {
        try
        {
            return Convert.FromBase64String(base64);
        }
        catch (FormatException)
        {
            return null;
        }
    }
}