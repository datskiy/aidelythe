using Aidelythe.Shared.RegularExpressions;

namespace Aidelythe.Shared.Tests.RegularExpressions;

public sealed class RegexHelperTests
{
    [Fact]
    public void CreateConfigured_WhenPatternIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var nullPattern = (string)null!;

        // Act
        var tryCreateConfigured = () => RegexHelper.CreateConfigured(nullPattern);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreateConfigured);
    }

    [Fact]
    public void CreateConfigured_WhenPatternIsEmptyOrWhiteSpace_ShouldThrowArgumentException()
    {
        // Arrange
        var emptyPattern = "";
        var whiteSpacePattern = " ";

        // Act
        var tryCreateConfiguredWithEmptyPattern = () => RegexHelper.CreateConfigured(emptyPattern);
        var tryCreateConfiguredWithWhiteSpacePattern = () => RegexHelper.CreateConfigured(whiteSpacePattern);

        // Assert
        Assert.Throws<ArgumentException>(tryCreateConfiguredWithEmptyPattern);
        Assert.Throws<ArgumentException>(tryCreateConfiguredWithWhiteSpacePattern);

    }

    [Fact]
    public void CreateConfigured_WhenPatternIsValid_ShouldReturnConfiguredRegex()
    {
        // Arrange
        const string pattern = @"^\d+$";

        // Act
        var regex = RegexHelper.CreateConfigured(pattern);

        // Assert
        Assert.Equal(pattern, regex.ToString());
        Assert.Equal(
            RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.NonBacktracking,
            regex.Options);
        Assert.Equal(TimeSpan.FromMilliseconds(100), regex.MatchTimeout);
    }
}