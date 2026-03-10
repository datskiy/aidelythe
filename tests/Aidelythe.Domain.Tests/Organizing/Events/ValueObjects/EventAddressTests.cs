using Aidelythe.Domain.Organizing.Events.ValueObjects;

namespace Aidelythe.Domain.Tests.Organizing.Events.ValueObjects;

public sealed class EventAddressTests
{
    [Fact]
    public void Ctor_WhenCountryIsNullOrWhiteSpace_ShouldThrowArgumentException()
    {
        // Arrange
        var nullCountry = (string?)null;
        var emptyCountry = string.Empty;
        var whiteSpaceCountry = " ";

        // Act
        var tryCreateWithNullCountry = () => new EventAddress(nullCountry!);
        var tryCreateWithEmptyCountry = () => new EventAddress(emptyCountry);
        var tryCreateWithWhiteSpaceCountry = () => new EventAddress(whiteSpaceCountry);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreateWithNullCountry);
        Assert.Throws<ArgumentException>(tryCreateWithEmptyCountry);
        Assert.Throws<ArgumentException>(tryCreateWithWhiteSpaceCountry);
    }

    [Fact]
    public void Ctor_WhenRegionIsWhiteSpace_ShouldThrowArgumentException()
    {
        // Arrange
        var emptyRegion = string.Empty;
        var whiteSpaceRegion = " ";

        // Act
        var tryCreateWithEmptyRegion = () => new EventAddress(emptyRegion);
        var tryCreateWithWhiteSpaceRegion = () => new EventAddress(whiteSpaceRegion);

        // Assert
        Assert.Throws<ArgumentException>(tryCreateWithEmptyRegion);
        Assert.Throws<ArgumentException>(tryCreateWithWhiteSpaceRegion);
    }

    [Fact]
    public void Ctor_WhenCityIsWhiteSpace_ShouldThrowArgumentException()
    {
        // Arrange
        var emptyCity = string.Empty;
        var whiteSpaceCity  = " ";

        // Act
        var tryCreateWithEmptyCity = () => new EventAddress(emptyCity);
        var tryCreateWithWhiteSpaceCity = () => new EventAddress(whiteSpaceCity);

        // Assert
        Assert.Throws<ArgumentException>(tryCreateWithEmptyCity);
        Assert.Throws<ArgumentException>(tryCreateWithWhiteSpaceCity);
    }

    [Fact]
    public void Ctor_WhenCountryOrRegionOrCityIsLongerThanMaximumLength_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var country = "Test Country";
        var region = "Test Region";
        var city = "Test City";
        var longCountry = new string('x', EventAddress.MaximumAreaNameLength + 1);
        var longRegion = new string('x', EventAddress.MaximumAreaNameLength + 1);
        var longCity = new string('x', EventAddress.MaximumAreaNameLength + 1);

        // Act
        var tryCreateWithLongCountry = () => new EventAddress(longCountry, region, city);
        var tryCreateWithLongRegion = () => new EventAddress(country, longRegion, city);
        var tryCreateWithLongCity = () => new EventAddress(country, region, longCity);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryCreateWithLongCountry);
        Assert.Throws<ArgumentOutOfRangeException>(tryCreateWithLongRegion);
        Assert.Throws<ArgumentOutOfRangeException>(tryCreateWithLongCity);
    }

    [Fact]
    public void Ctor_WhenPostalCodeIsWhiteSpace_ShouldThrowArgumentException()
    {
        // Arrange
        var emptyPostalCode = string.Empty;
        var whiteSpacePostalCode = " ";

        // Act
        var tryCreateWithEmptyPostalCode = () => new EventAddress(emptyPostalCode);
        var tryCreateWithWhiteSpacePostalCode = () => new EventAddress(whiteSpacePostalCode);

        // Assert
        Assert.Throws<ArgumentException>(tryCreateWithEmptyPostalCode);
        Assert.Throws<ArgumentException>(tryCreateWithWhiteSpacePostalCode);
    }

    [Fact]
    public void Ctor_WhenPostalCodeIsLongerThanMaximumLength_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var country = "Test Country";
        var longPostalCode = new string('x', EventAddress.MaximumPostalCodeLength + 1);

        // Act
        var tryCreate = () => new EventAddress(country, postalCode: longPostalCode);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenPostalCodeFormatIsInvalid_ShouldThrowArgumentException()
    {
        // Arrange
        var country = "Test Country";
        var invalidFormatPostalCode = "%!@#$%^&*()";

        // Act
        var tryCreate = () => new EventAddress(country, postalCode: invalidFormatPostalCode);

        // Assert
        Assert.Throws<ArgumentException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenStreetIsWhiteSpace_ShouldThrowArgumentException()
    {
        // Arrange
        var emptyStreet = string.Empty;
        var whiteSpaceStreet = " ";

        // Act
        var tryCreateWithEmptyStreet = () => new EventAddress(emptyStreet);
        var tryCreateWithWhiteSpaceStreet = () => new EventAddress(whiteSpaceStreet);

        // Assert
        Assert.Throws<ArgumentException>(tryCreateWithEmptyStreet);
        Assert.Throws<ArgumentException>(tryCreateWithWhiteSpaceStreet);
    }

    [Fact]
    public void Ctor_WhenStreetIsLongerThanMaximumLength_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var country = "Test Country";
        var longStreet = new string('x', EventAddress.MaximumStreetNameLength + 1);

        // Act
        var tryCreate = () => new EventAddress(country, street: longStreet);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenArgumentsAreValid_ShouldReturnEventAddress()
    {
        // Arrange
        var country = "Test Country";
        var region = "Test Region";
        var city = "Test City";
        var postalCode = "13-37";
        var street = "Test Street";

        // Act
        var eventAddress= new EventAddress(
            country,
            region,
            city,
            postalCode,
            street);

        // Assert
        Assert.True(eventAddress is not null);
    }

    [Fact]
    public void ToString_ShouldReturnCommaSeparatedValues()
    {
        // Arrange
        var eventAddress= new EventAddress(
            "Test Country",
            "Test Region",
            "Test City",
            "13-37",
            "Test Street");

        // Act
        var eventAddressString = eventAddress.ToString();

        // Assert
        Assert.Equal(
            eventAddressString,
            string.Join(", ",
                eventAddress.Country,
                eventAddress.Region,
                eventAddress.City,
                eventAddress.PostalCode,
                eventAddress.Street));
    }
}