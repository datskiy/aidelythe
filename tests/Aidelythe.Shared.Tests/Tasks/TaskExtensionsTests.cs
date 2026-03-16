using Aidelythe.Shared.Tasks;

namespace Aidelythe.Shared.Tests.Tasks;

public sealed class TaskExtensionsTests
{
    [Fact]
    public async Task ToTask_WhenValueIsProvided_ShouldReturnCompletedTaskWithValueAsResult()
    {
        // Arrange
        var value = -1;

        // Act
        var result = await value.ToTask();

        // Assert
        Assert.Equal(value, result);
    }

    [Fact]
    public void ToValueTask_WhenTaskIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var nullTask = (Task?)null;

        // Act
        var tryToValueTask = () => { nullTask!.ToValueTask(); };

        // Assert
        Assert.Throws<ArgumentNullException>(tryToValueTask);
    }

    [Fact]
    public void ToValueTask_WhenTaskIsProvided_ShouldReturnValueTaskFromThisTask()
    {
        // Arrange
        var task = Task.FromResult(-1);

        // Act
        var valueTask = task.ToValueTask();

        // Assert
        Assert.Equal(task, valueTask.AsTask());
    }
}