using Aidelythe.Shared.DiscriminatedUnion;

namespace Aidelythe.Application._Common.Discriminants;

/// <summary>
/// Represents an invalid date range.
/// </summary>
public readonly record struct InvalidDateRange : IDiscriminant;