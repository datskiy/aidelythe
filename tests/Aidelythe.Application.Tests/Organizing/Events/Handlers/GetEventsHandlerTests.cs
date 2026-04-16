using Aidelythe.Application.Organizing.Events.Handlers;
using Aidelythe.Application.Organizing.Events.Queries;

namespace Aidelythe.Application.Tests.Organizing.Events.Handlers;

public sealed class GetEventsHandlerTests
{
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    [Fact]
    public async Task Handle_WhenQueryIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sut = CreateSut();
        var nullQuery = (GetEventsQuery?)null;

        // Act
        var tryHandle = () => sut.Handle(
            nullQuery!,
            _cancellationToken);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(tryHandle);
    }

    private GetEventsHandler CreateSut()
    {
        return new GetEventsHandler();
    }
}