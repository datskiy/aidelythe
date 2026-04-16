using Aidelythe.Shared.DiscriminatedUnion;

namespace Aidelythe.Application._Common.Discriminants;

/// <summary>
/// Represents an entity or resource that has expired.
/// </summary>
public readonly record struct Expired : IDiscriminant;