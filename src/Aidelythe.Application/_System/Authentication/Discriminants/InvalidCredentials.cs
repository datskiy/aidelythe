using Aidelythe.Shared.DiscriminatedUnion;

namespace Aidelythe.Application._System.Authentication.Discriminants;

/// <summary>
/// Represents invalid credentials when logging in.
/// </summary>
public readonly record struct InvalidCredentials : IDiscriminant;