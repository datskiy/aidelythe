using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.Services;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Infrastructure._Common.Settings;

namespace Aidelythe.Infrastructure._System.Authentication.Services;

/// <summary>
/// Represents a service for managing refresh tokens.
/// </summary>
public sealed class RefreshTokenService : IRefreshTokenService
{
    private readonly IUserSessionRepository _userSessionRepository;

    private readonly RefreshTokenSettings _refreshTokenSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenService"/> class.
    /// </summary>
    /// <param name="userSessionRepository">The instance of <see cref="IUserSessionRepository"/>.</param>
    /// <param name="refreshTokenOptions">The instance of <see cref="IOptions{RefreshTokenSettings}"/>.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="userSessionRepository"/> or <paramref name="refreshTokenOptions"/> is null.
    /// </exception>
    public RefreshTokenService(
        IUserSessionRepository userSessionRepository,
        IOptions<RefreshTokenSettings> refreshTokenOptions)
    {
        ThrowIfNull(userSessionRepository);
        ThrowIfNull(refreshTokenOptions);

        _userSessionRepository = userSessionRepository;
        _refreshTokenSettings = refreshTokenOptions.Value;
    }

    /// <inheritdoc/>
    public RefreshTokenDescriptor Generate()
    {
        var tokenBytes = RandomNumberGenerator.GetBytes(_refreshTokenSettings.ByteCount);
        var token = Convert.ToBase64String(tokenBytes);
        var refreshToken = new RefreshToken(token);

        var refreshTokenHash = HashToken(tokenBytes);
        var expiresAt = DateTime.UtcNow.AddSeconds(_refreshTokenSettings.ExpiresInSeconds);

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