using Aidelythe.Shared.DiscriminatedUnion;

namespace Aidelythe.Application._System.Authentication.Discriminants;

/// <summary>
/// Represents a successful password verification where rehashing of the password is required.
/// </summary>
public readonly record struct SuccessRehashNeeded : IDiscriminant;