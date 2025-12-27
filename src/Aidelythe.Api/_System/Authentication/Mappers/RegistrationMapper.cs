using Aidelythe.Api._System.Authentication.Requests;
using Aidelythe.Application._System.Authentication.Commands;

namespace Aidelythe.Api._System.Authentication.Mappers;

/// <summary>
/// Provides mapping methods for registration.
/// </summary>
[Mapper]
public static partial class RegistrationMapper
{
    /// <summary>
    /// Maps the <see cref="RegisterRequest"/> instance to a <see cref="RegisterCommand"/> object.
    /// </summary>
    /// <param name="request">The <see cref="RegisterRequest"/> to map.</param>
    /// <returns>
    /// The mapped <see cref="RegisterCommand"/>.
    /// </returns>
    public static partial RegisterCommand ToCommand(this RegisterRequest request);
}