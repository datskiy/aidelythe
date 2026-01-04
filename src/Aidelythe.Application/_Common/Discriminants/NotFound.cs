using Aidelythe.Shared.Unions;

namespace Aidelythe.Application._Common.Discriminants;

/// <summary>
/// Represents an entity or resource that could not be found.
/// </summary>
public readonly record struct NotFound : IDiscriminant;