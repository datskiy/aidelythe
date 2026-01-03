using Aidelythe.Shared.DiscriminatedUnion;

namespace Aidelythe.Application._Common.Discriminants;

/// <summary>
/// Represents a failed operation outcome.
/// </summary>
public readonly record struct Failure : IDiscriminant;