using Aidelythe.Api._Common.Http.Metadata;

namespace Aidelythe.Api.Tests._Common.Http.Metadata;

public sealed class LinkHeaderBuilderTests
{
    private readonly LinkGenerator _linkGenerator = Substitute.For<LinkGenerator>();
    private readonly DefaultHttpContext _httpContext = CreateHttpContextWithEndpoint();

    [Fact]
    public void Ctor_WhenHttpContextIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var nullHttpContext = (HttpContext?)null;

        // Act
        var tryCreate = () => new LinkHeaderBuilder(
            nullHttpContext!,
            _linkGenerator);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenLinkGeneratorIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        var nullLinkGenerator = (LinkGenerator?)null;

        // Act
        var tryCreate = () => new LinkHeaderBuilder(
            httpContext,
            nullLinkGenerator!);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenEndpointCannotBeRetrieved_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();

        // Act
        var tryCreate = () => new LinkHeaderBuilder(
            httpContext,
            _linkGenerator);

        // Assert
        Assert.Throws<InvalidOperationException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenActionDescriptorCannotBeRetrieved_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        httpContext.SetEndpoint(new Endpoint(
            _ => Task.CompletedTask,
            new EndpointMetadataCollection(),
            "test-endpoint"));

        // Act
        var tryCreate = () => new LinkHeaderBuilder(
            httpContext,
            _linkGenerator);

        // Assert
        Assert.Throws<InvalidOperationException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenArgumentsAreValid_ShouldReturnLinkHeaderBuilder()
    {
        // Act
        var linkHeaderBuilder = new LinkHeaderBuilder(
            _httpContext,
            _linkGenerator);

        // Assert
        Assert.NotNull(linkHeaderBuilder);
    }

    [Fact]
    public void Add_WhenRelIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sut = CreateSut();
        var nullRel = (string?)null;

        // Act
        var tryAdd = () => sut.Add(new LinkRouteParams(0, 10), nullRel!);

        // Assert
        Assert.Throws<ArgumentNullException>(tryAdd);
    }

    [Fact]
    public void Add_WhenUriCannotBeGenerated_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var sut = CreateSut();

        _linkGenerator
            .GetUriByAddress<RouteValuesAddress>(
                _httpContext,
                Arg.Any<RouteValuesAddress>(),
                Arg.Any<RouteValueDictionary>(),
                Arg.Any<RouteValueDictionary?>(),
                Arg.Any<string?>(),
                Arg.Any<HostString?>(),
                Arg.Any<PathString?>(),
                Arg.Any<FragmentString>(),
                Arg.Any<LinkOptions?>())
            .Returns("   ");

        // Act
        var tryAdd = () => sut.Add(new LinkRouteParams(0, 10), "next");

        // Assert
        Assert.Throws<InvalidOperationException>(tryAdd);
    }

    [Fact]
    public void Add_WhenUriCanBeGenerated_ShouldAppendFormattedLink()
    {
        // Arrange
        var sut = CreateSut();

        _linkGenerator
            .GetUriByAddress<RouteValuesAddress>(
                _httpContext,
                Arg.Any<RouteValuesAddress>(),
                Arg.Any<RouteValueDictionary>(),
                Arg.Any<RouteValueDictionary?>(),
                Arg.Any<string?>(),
                Arg.Any<HostString?>(),
                Arg.Any<PathString?>(),
                Arg.Any<FragmentString>(),
                Arg.Any<LinkOptions?>())
            .Returns("https://example.com/events?offset=0&limit=10");

        // Act
        var result = sut
            .Add(new LinkRouteParams(0, 10), "self")
            .Build();

        // Assert
        Assert.Equal("<https://example.com/events?offset=0&limit=10>; rel=\"self\"", result);
    }

    [Fact]
    public void Add_WhenCalledMultipleTimes_ShouldJoinLinksWithCommaSeparator()
    {
        // Arrange
        var sut = CreateSut();

        _linkGenerator
            .GetUriByAddress<RouteValuesAddress>(
                _httpContext,
                Arg.Any<RouteValuesAddress>(),
                Arg.Any<RouteValueDictionary>(),
                Arg.Any<RouteValueDictionary?>(),
                Arg.Any<string?>(),
                Arg.Any<HostString?>(),
                Arg.Any<PathString?>(),
                Arg.Any<FragmentString>(),
                Arg.Any<LinkOptions?>())
            .Returns(
                "https://example.com/events?offset=0&limit=10",
                "https://example.com/events?offset=10&limit=10");

        // Act
        var result = sut
            .Add(new LinkRouteParams(0, 10), "self")
            .Add(new LinkRouteParams(10, 10), "next")
            .Build();

        // Assert
        Assert.Equal(
            "<https://example.com/events?offset=0&limit=10>; rel=\"self\", <https://example.com/events?offset=10&limit=10>; rel=\"next\"",
            result);
    }

    [Fact]
    public void AddIf_WhenRelIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var httpContext = CreateHttpContextWithEndpoint();
        var linkGenerator = Substitute.For<LinkGenerator>();
        var sut = new LinkHeaderBuilder(httpContext, linkGenerator);
        var nullRel = (string?)null;

        // Act
        var tryAddIf = () => sut.AddIf(true, new LinkRouteParams(10, 10), nullRel!);

        // Assert
        Assert.Throws<ArgumentNullException>(tryAddIf);
    }

    [Fact]
    public void AddIf_WhenConditionIsFalse_ShouldNotAddLink()
    {
        // Arrange
        var httpContext = CreateHttpContextWithEndpoint();
        var linkGenerator = Substitute.For<LinkGenerator>();
        var sut = new LinkHeaderBuilder(httpContext, linkGenerator);

        // Act
        var result = sut
            .AddIf(condition: false, new LinkRouteParams(10, 10), "next")
            .Build();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void AddIf_WhenConditionIsTrue_ShouldAddLink()
    {
        // Arrange
        var sut = CreateSut();

        _linkGenerator
            .GetUriByAddress<RouteValuesAddress>(
                _httpContext,
                Arg.Any<RouteValuesAddress>(),
                Arg.Any<RouteValueDictionary>(),
                Arg.Any<RouteValueDictionary?>(),
                Arg.Any<string?>(),
                Arg.Any<HostString?>(),
                Arg.Any<PathString?>(),
                Arg.Any<FragmentString>(),
                Arg.Any<LinkOptions?>())
            .Returns("https://example.com/events?offset=10&limit=10");

        // Act
        var result = sut
            .AddIf(condition: true, new LinkRouteParams(10, 10), "next")
            .Build();

        // Assert
        Assert.Equal("<https://example.com/events?offset=10&limit=10>; rel=\"next\"", result);
    }

    [Fact]
    public void Build_WhenNoLinksWereAdded_ShouldReturnEmptyString()
    {
        // Arrange
        var sut = CreateSut();

        // Act
        var result = sut.Build();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Build_WhenLinksWereAdded_ShouldReturnCommaSeparatedHeaderValue()
    {
        // Arrange
        var sut = CreateSut();

        _linkGenerator
            .GetUriByAddress<RouteValuesAddress>(
                _httpContext,
                Arg.Any<RouteValuesAddress>(),
                Arg.Any<RouteValueDictionary>(),
                Arg.Any<RouteValueDictionary?>(),
                Arg.Any<string?>(),
                Arg.Any<HostString?>(),
                Arg.Any<PathString?>(),
                Arg.Any<FragmentString>(),
                Arg.Any<LinkOptions?>())
            .Returns(
                "https://example.com/events?offset=0&limit=10",
                "https://example.com/events?offset=10&limit=10");

        sut.Add(new LinkRouteParams(0, 10), "self");
        sut.Add(new LinkRouteParams(10, 10), "next");

        // Act
        var result = sut.Build();

        // Assert
        Assert.Equal(
            "<https://example.com/events?offset=0&limit=10>; rel=\"self\", <https://example.com/events?offset=10&limit=10>; rel=\"next\"",
            result);
    }

    private LinkHeaderBuilder CreateSut()
    {
        return new LinkHeaderBuilder(_httpContext, _linkGenerator);
    }

    private static DefaultHttpContext CreateHttpContextWithEndpoint()
    {
        var httpContext = new DefaultHttpContext();

        var actionDescriptor = new ControllerActionDescriptor
        {
            ControllerName = "Events",
            ActionName = "List"
        };

        httpContext.SetEndpoint(new Endpoint(
            _ => Task.CompletedTask,
            new EndpointMetadataCollection(actionDescriptor),
            "test-endpoint"));

        return httpContext;
    }
}