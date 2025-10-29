using Aidelythe.Shared.DiscriminatedUnion;

namespace Aidelythe.Application._Common.Discriminants;

/// <summary>
/// Represents a duplicate title of an entity.
/// </summary>
public readonly record struct DuplicateTitle : IDiscriminant;