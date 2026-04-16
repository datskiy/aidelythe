using Aidelythe.Shared.DiscriminatedUnion;

namespace Aidelythe.Application._Common.Discriminants;

/// <summary>
/// Represents a successful operation outcome.
/// </summary>
public readonly record struct Success : IDiscriminant;