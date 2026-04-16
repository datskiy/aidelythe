using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application._System.Authentication.Discriminants;

namespace Aidelythe.Application._System.Authentication.Results;

/// <summary>
/// Represents the result of registering a user.
/// </summary>
public sealed class RegisterResult
{
    /// <summary>
    /// Gets the discriminated union containing all possible outcomes.
    /// </summary>
    public OneOf<Guid, AlreadyExists, MissingContactMethod> Union { get; }

    private RegisterResult(OneOf<Guid, AlreadyExists, MissingContactMethod> union)
    {
        Union = union;
    }

    public static implicit operator RegisterResult(Guid userId)
    {
        return new RegisterResult(userId);
    }

    public static implicit operator RegisterResult(AlreadyExists alreadyExists)
    {
        return new RegisterResult(alreadyExists);
    }

    public static implicit operator RegisterResult(MissingContactMethod missingContactMethod)
    {
        return new RegisterResult(missingContactMethod);
    }
}