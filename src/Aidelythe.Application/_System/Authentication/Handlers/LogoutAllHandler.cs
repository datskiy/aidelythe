using Aidelythe.Application._Common.Persistence;
using Aidelythe.Application._System.Authentication.Commands;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Handlers;

/// <summary>
/// Represents a command handler for logging out all user sessions.
/// </summary>
public sealed class LogoutAllHandler : IRequestHandler<LogoutAllCommand>
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserSessionRepository _userSessionRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="LogoutAllHandler"/> class.
    /// </summary>
    /// <param name="logger">The instance of <see cref="ILogger"/>.</param>
    /// <param name="unitOfWork">The instance of <see cref="IUnitOfWork"/>.</param>
    /// <param name="userSessionRepository">The instance of <see cref="IUserSessionRepository"/>.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="logger"/>, <paramref name="unitOfWork"/> or
    /// <paramref name="userSessionRepository"/> is null.
    /// </exception>
    public LogoutAllHandler(
        ILogger<LogoutAllHandler> logger,
        IUnitOfWork unitOfWork,
        IUserSessionRepository userSessionRepository)
    {
        ThrowIfNull(logger);
        ThrowIfNull(unitOfWork);
        ThrowIfNull(userSessionRepository);

        _logger = logger;
        _unitOfWork = unitOfWork;
        _userSessionRepository = userSessionRepository;
    }

    /// <summary>
    /// Handles the given <see cref="LogoutAllCommand"/>.
    /// </summary>
    /// <param name="request">The command to log out all user sessions.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the result of logging out all user sessions.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="request"/> is null.</exception>
    public async Task Handle(
        LogoutAllCommand request,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(request);

        var userId = new UserId(request.UserId);

        var userSessionCount = await _userSessionRepository.CountAsync(userId, cancellationToken);
        if (userSessionCount == 0)
        {
            _logger.LogInformation("Logout-all attempted for user {UserId} with 0 sessions", userId);
            return;
        }

        await _userSessionRepository.DeleteAsync(userId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "User {UserId} successfully logged out of all {SessionCount} sessions",
            userId,
            userSessionCount);
    }
}