using Aidelythe.Shared.DiscriminatedUnion;

namespace Aidelythe.Application._Common.Discriminants;

/// <summary>
/// Represents a duplicate entity.
/// </summary>
public readonly record struct AlreadyExists : IDiscriminant;