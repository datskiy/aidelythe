namespace Aidelythe.Api._Common.Http.Metadata;

/// <summary>
/// Represents route parameters used for constructing a link for Link header.
/// </summary>
/// <param name="Offset">The starting index of the current page in the collection of items.</param>
/// <param name="Limit">The maximum number of items included in the current page.</param>
public readonly record struct LinkRouteParams(int Offset, int Limit);