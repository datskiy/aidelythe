using Aidelythe.Api._Common.Sorting;

namespace Aidelythe.Api.Tests._Common.Http.Sorting;

public sealed class SortingHelperTests
{
    [Fact]
    public void ParseSortingFields_WhenSortByIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var nullSortBy = (string?)null;
        var sortableFieldDictionary = CreateSortableFieldDictionary();

        // Act
        var tryParseSortingFields = () => SortingHelper.ParseSortingFields(
            nullSortBy!,
            sortableFieldDictionary);

        // Assert
        Assert.Throws<ArgumentNullException>(tryParseSortingFields);
    }

    [Fact]
    public void ParseSortingFields_WhenSortableFieldDictionaryIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sortBy = "name:asc";
        var nullSortableFieldDictionary = (IReadOnlyDictionary<string, string>?)null;

        // Act
        var tryParseSortingFields = () => SortingHelper.ParseSortingFields(
            sortBy,
            nullSortableFieldDictionary!);

        // Assert
        Assert.Throws<ArgumentNullException>(tryParseSortingFields);
    }

    [Fact]
    public void ParseSortingFields_WhenSortableFieldDictionaryIsEmpty_ShouldThrowArgumentException()
    {
        // Arrange
        var sortBy = "name:asc";
        var emptySortableFieldDictionary = new Dictionary<string, string>();

        // Act
        var tryParseSortingFields = () => SortingHelper.ParseSortingFields(
            sortBy,
            emptySortableFieldDictionary);

        // Assert
        Assert.Throws<ArgumentException>(tryParseSortingFields);
    }

    [Fact]
    public void ParseSortingFields_WhenSortingDirectionIsInvalid_ShouldThrowArgumentException()
    {
        // Arrange
        var sortByWithInvalidSortingDirection = "name:sideways";
        var sortableFieldDictionary = CreateSortableFieldDictionary();

        // Act
        var tryParseSortingFields = () => SortingHelper.ParseSortingFields(
            sortByWithInvalidSortingDirection,
            sortableFieldDictionary);

        // Assert
        Assert.Throws<ArgumentException>(tryParseSortingFields);
    }

    [Fact]
    public void ParseSortingFields_WhenSortFieldIsUnknown_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var sortByWithUnknownField = "unknown:asc";
        var sortableFieldDictionary = CreateSortableFieldDictionary();

        // Act
        var tryParseSortingFields = () => SortingHelper.ParseSortingFields(
            sortByWithUnknownField,
            sortableFieldDictionary);

        // Assert
        Assert.Throws<KeyNotFoundException>(tryParseSortingFields);
    }

    [Fact]
    public void ParseSortingFields_WhenSortingDirectionHasDifferentCase_ShouldParseSuccessfully()
    {
        // Arrange
        var sortBy = "name:AsC,createdAt:DeSc";
        var sortableFieldDictionary = CreateSortableFieldDictionary();

        // Act
        var result = SortingHelper.ParseSortingFields(
            sortBy, sortableFieldDictionary)
            .ToArray();

        // Assert
        Assert.Equal(2, result.Length);

        Assert.Equal("Name", result[0].PropertyName);
        Assert.False(result[0].IsDescending);

        Assert.Equal("CreatedAt", result[1].PropertyName);
        Assert.True(result[1].IsDescending);
    }

    [Fact]
    public void ParseSortingFields_WhenArgumentsAreValid_ShouldReturnAllSortFieldsInOrder()
    {
        // Arrange
        var sortBy = "name:asc,createdAt:desc";
        var sortableFieldDictionary = CreateSortableFieldDictionary();

        // Act
        var result = SortingHelper
            .ParseSortingFields(sortBy, sortableFieldDictionary)
            .ToArray();

        // Assert
        Assert.Equal(2, result.Length);

        Assert.Equal("Name", result[0].PropertyName);
        Assert.False(result[0].IsDescending);

        Assert.Equal("CreatedAt", result[1].PropertyName);
        Assert.True(result[1].IsDescending);
    }

    private static IReadOnlyDictionary<string, string> CreateSortableFieldDictionary()
    {
        return new Dictionary<string, string>
        {
            ["name"] = "Name",
            ["createdAt"] = "CreatedAt"
        };
    }
}