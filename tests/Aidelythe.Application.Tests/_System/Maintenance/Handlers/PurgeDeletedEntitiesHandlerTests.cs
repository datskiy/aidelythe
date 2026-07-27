using Aidelythe.Application._Common.Persistence;
using Aidelythe.Application._System.Maintenance.Commands;
using Aidelythe.Application._System.Maintenance.Handlers;
using Aidelythe.Application.Organizing.Events.Repositories;
using Aidelythe.Shared.Settings;

namespace Aidelythe.Application.Tests._System.Maintenance.Handlers;

public sealed class PurgeDeletedEntitiesHandlerTests
{
    private readonly ILogger<PurgeDeletedEntitiesHandler> _logger = Substitute.For<ILogger<PurgeDeletedEntitiesHandler>>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IEventRepository _eventRepository = Substitute.For<IEventRepository>();

    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    [Fact]
    public async Task Handle_WhenCommandIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sut = CreateSut();
        var nullCommand = (PurgeDeletedEntitiesCommand?)null;

        // Act
        var tryHandle = () => sut.Handle(
            nullCommand!,
            _cancellationToken);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(tryHandle);
    }

    [Fact]
    public async Task Handle_WhenCommandIsValid_ShouldPurgeDeletedEvents()
    {
        // Arrange
        var sut = CreateSut();
        var command = new PurgeDeletedEntitiesCommand();

        _eventRepository
            .PurgeDeletedOlderThanAsync(Arg.Any<DateOnly>(), Arg.Any<CancellationToken>())
            .Returns(3);

        // Act
        await sut.Handle(
            command,
            _cancellationToken);

        // Assert
        await _unitOfWork
            .Received(requiredNumberOfCalls: 1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    private PurgeDeletedEntitiesHandler CreateSut()
    {
        return new PurgeDeletedEntitiesHandler(
            _logger,
            _unitOfWork,
            _eventRepository,
            TimeProvider.System,
            Options.Create(new MaintenanceSettings
            {
                DeletedEntityRetentionDays = 14
            }));
    }
}
