using Aidelythe.Application.Organizing.Events.Commands;
using Aidelythe.Application.Organizing.Events.Handlers;

namespace Aidelythe.Application.Tests.Organizing.Events.Handlers;

public sealed class DeleteEventHandlerTests
{
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    [Fact]
    public async Task Handle_WhenCommandIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sut = CreateSut();
        var nullCommand = (DeleteEventCommand?)null;

        // Act
        var tryHandle = () => sut.Handle(
            nullCommand!,
            _cancellationToken);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(tryHandle);
    }

    private DeleteEventHandler CreateSut()
    {
        return new DeleteEventHandler();
    }
}