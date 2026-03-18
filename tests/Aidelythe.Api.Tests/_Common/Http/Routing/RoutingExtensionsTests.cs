using Aidelythe.Api._Common.Http.Routing;

namespace Aidelythe.Api.Tests._Common.Http.Routing;

public sealed class RoutingExtensionsTests
{
    [Fact]
    public void RemoveControllerSuffix_WhenControllerNameIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var nullControllerName = (string?)null;

        // Act
        var tryRemoveControllerSuffix = () => nullControllerName!.RemoveControllerSuffix();

        // Assert
        Assert.Throws<ArgumentNullException>(tryRemoveControllerSuffix);
    }

    [Fact]
    public void RemoveControllerSuffix_WhenControllerNameIsEmptyOrWhiteSpace_ShouldThrowArgumentException()
    {
        // Arrange
        var emptyControllerName = "";
        var whiteSpaceControllerName = " ";

        // Act
        var tryRemoveControllerSuffixWithEmptyControllerName =
            () => emptyControllerName.RemoveControllerSuffix();

        var tryRemoveControllerSuffixWithWhiteSpaceControllerName =
            () => whiteSpaceControllerName.RemoveControllerSuffix();

        // Assert
        Assert.Throws<ArgumentException>(tryRemoveControllerSuffixWithEmptyControllerName);
        Assert.Throws<ArgumentException>(tryRemoveControllerSuffixWithWhiteSpaceControllerName);
    }

    [Fact]
    public void RemoveControllerSuffix_WhenControllerNameEndsWithController_ShouldRemoveSuffix()
    {
        // Arrange
        var controllerName = "EventController";

        // Act
        var result = controllerName.RemoveControllerSuffix();

        // Assert
        Assert.Equal("Event", result);
    }

    [Fact]
    public void RemoveControllerSuffix_WhenControllerNameEndsWithControllerInDifferentCase_ShouldRemoveSuffix()
    {
        // Arrange
        var controllerName = "EventcOnTrOlLeR";

        // Act
        var result = controllerName.RemoveControllerSuffix();

        // Assert
        Assert.Equal("Event", result);
    }

    [Fact]
    public void RemoveControllerSuffix_WhenControllerNameDoesNotEndWithController_ShouldReturnOriginalValue()
    {
        // Arrange
        var controllerName = "Event";

        // Act
        var result = controllerName.RemoveControllerSuffix();

        // Assert
        Assert.Equal(controllerName, result);
    }

    [Fact]
    public void RemoveControllerSuffix_WhenControllerNameIsOnlyController_ShouldReturnEmptyString()
    {
        // Arrange
        var controllerName = "Controller";

        // Act
        var result = controllerName.RemoveControllerSuffix();

        // Assert
        Assert.Equal(string.Empty, result);
    }
}