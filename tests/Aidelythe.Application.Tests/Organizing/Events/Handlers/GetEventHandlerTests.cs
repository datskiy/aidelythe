using Aidelythe.Application.Organizing.Events.Handlers;
using Aidelythe.Application.Organizing.Events.Queries;

namespace Aidelythe.Application.Tests.Organizing.Events.Handlers;

public sealed class GetEventHandlerTests
{
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    [Fact]
    public async Task Handle_WhenQueryIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sut = CreateSut();
        var nullQuery = (GetEventQuery?)null;

        // Act
        var tryHandle = () => sut.Handle(
            nullQuery!,
            _cancellationToken);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(tryHandle);
    }

    private GetEventHandler CreateSut()
    {
        return new GetEventHandler();
    }
}