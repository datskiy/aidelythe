using Aidelythe.Shared.Unions;

namespace Aidelythe.Application._System.Authentication.Discriminants;

/// <summary>
/// Represents invalid token when refreshing a token pair.
/// </summary>
public readonly record struct InvalidToken : IDiscriminant;