using Aidelythe.Application._Common.Persistence;
using Aidelythe.Application._System.Maintenance.Commands;
using Aidelythe.Application.Organizing.Events.Repositories;
using Aidelythe.Shared.Settings;

namespace Aidelythe.Application._System.Maintenance.Handlers;

/// <summary>
/// Represents a command handler for purging deleted entities.
/// </summary>
public sealed partial class PurgeDeletedEntitiesHandler : IRequestHandler<PurgeDeletedEntitiesCommand>
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IEventRepository _eventRepository;

    private readonly TimeProvider _timeProvider;
    private readonly MaintenanceSettings _maintenanceSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="PurgeDeletedEntitiesHandler"/> class.
    /// </summary>
    /// <param name="logger">The instance of <see cref="ILogger"/>.</param>
    /// <param name="unitOfWork">The instance of <see cref="IUnitOfWork"/>.</param>
    /// <param name="eventRepository">The instance of <see cref="IEventRepository"/>.</param>
    /// <param name="timeProvider">The instance of <see cref="TimeProvider"/>.</param>
    /// <param name="maintenanceOptions">The instance of <see cref="IOptions{MaintenanceSettings}"/>.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="logger"/>, <paramref name="unitOfWork"/>, <paramref name="eventRepository"/>,
    /// <paramref name="timeProvider"/> or <paramref name="maintenanceOptions"/> is null.
    /// </exception>
    public PurgeDeletedEntitiesHandler(
        ILogger<PurgeDeletedEntitiesHandler> logger,
        IUnitOfWork unitOfWork,
        IEventRepository eventRepository,
        TimeProvider timeProvider,
        IOptions<MaintenanceSettings> maintenanceOptions)
    {
        ThrowIfNull(logger);
        ThrowIfNull(unitOfWork);
        ThrowIfNull(eventRepository);
        ThrowIfNull(timeProvider);
        ThrowIfNull(maintenanceOptions);

        _logger = logger;
        _unitOfWork = unitOfWork;
        _eventRepository = eventRepository;
        _timeProvider = timeProvider;
        _maintenanceSettings = maintenanceOptions.Value;
    }

    /// <summary>
    /// Handles the given <see cref="PurgeDeletedEntitiesCommand"/>.
    /// </summary>
    /// <param name="request">The command to purge deleted entities.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="request"/> is null.</exception>
    public async Task Handle(
        PurgeDeletedEntitiesCommand request,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(request);

        // TODO: ask and add distributed locking

        var cutoff = DateOnly.FromDateTime(_timeProvider
            .GetUtcNow()
            .AddDays(-_maintenanceSettings.DeletedEntityRetentionDays)
            .UtcDateTime);

        var deletedCount = await _eventRepository.PurgeDeletedOlderThanAsync(
            cutoff,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        LogEventsPurged(deletedCount, cutoff);
    }

    [LoggerMessage(LogLevel.Information, "Purged {DeletedCount} deleted events older than {Cutoff}")]
    private partial void LogEventsPurged(int deletedCount, DateOnly cutoff);
}