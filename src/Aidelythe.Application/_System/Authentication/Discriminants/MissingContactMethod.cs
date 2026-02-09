using Aidelythe.Shared.DiscriminatedUnion;

namespace Aidelythe.Application._System.Authentication.Discriminants;

/// <summary>
/// Represents a missing contact method for registration.
/// </summary>
public readonly record struct MissingContactMethod : IDiscriminant;