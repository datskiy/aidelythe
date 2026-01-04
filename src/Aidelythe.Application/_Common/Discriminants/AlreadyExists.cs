using Aidelythe.Shared.Unions;

namespace Aidelythe.Application._Common.Discriminants;

/// <summary>
/// Represents a duplicate entity.
/// </summary>
public readonly record struct AlreadyExists : IDiscriminant;