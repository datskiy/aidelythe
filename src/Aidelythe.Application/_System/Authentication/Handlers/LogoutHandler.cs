using Aidelythe.Application._Common.Persistence;
using Aidelythe.Application._System.Authentication.Commands;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Handlers;

/// <summary>
/// Represents a command handler for logging out the specified user session.
/// </summary>
public sealed class LogoutHandler : IRequestHandler<LogoutCommand>
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserSessionRepository _userSessionRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="LogoutHandler"/> class.
    /// </summary>
    /// <param name="logger">The instance of <see cref="ILogger"/>.</param>
    /// <param name="unitOfWork">The instance of <see cref="IUnitOfWork"/>.</param>
    /// <param name="userSessionRepository">The instance of <see cref="IUserSessionRepository"/>.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="logger"/>, <paramref name="unitOfWork"/> or
    /// <paramref name="userSessionRepository"/> is null.
    /// </exception>
    public LogoutHandler(
        ILogger<LogoutHandler> logger,
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
    /// Handles the given <see cref="LogoutCommand"/>.
    /// </summary>
    /// <param name="request">The command to log out the specified user session.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the result of logging out the specified user session.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="request"/> is null.</exception>
    public async Task Handle(
        LogoutCommand request,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(request);

        var userSessionId = new UserSessionId(request.UserSessionId);

        var userSession = await _userSessionRepository.GetAsync(userSessionId, cancellationToken);
        if (userSession is null)
        {
            _logger.LogInformation("Logout attempted for non-existent session {UserSessionId}", userSessionId);
            return;
        }

        await _userSessionRepository.DeleteAsync(userSessionId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "User {UserId} successfully logged out of the session {UserSessionId}",
            userSession.UserId,
            userSessionId);
    }
}