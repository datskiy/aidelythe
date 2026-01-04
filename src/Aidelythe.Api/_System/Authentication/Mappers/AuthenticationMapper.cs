using Aidelythe.Api._System.Authentication.Requests;
using Aidelythe.Api._System.Authentication.Responses;
using Aidelythe.Application._System.Authentication.Commands;
using Aidelythe.Application._System.Authentication.Projections;

namespace Aidelythe.Api._System.Authentication.Mappers;

/// <summary>
/// Provides mapping methods for authentication.
/// </summary>
[Mapper]
public static partial class AuthenticationMapper
{
    /// <summary>
    /// Maps the <see cref="RegisterRequest"/> instance to a <see cref="RegisterCommand"/> object.
    /// </summary>
    /// <param name="request">The <see cref="RegisterRequest"/> to map.</param>
    /// <returns>
    /// The mapped <see cref="RegisterCommand"/>.
    /// </returns>
    public static partial RegisterCommand ToCommand(this RegisterRequest request);

    /// <summary>
    /// Maps the <see cref="LoginRequest"/> instance to a <see cref="LoginCommand"/> object.
    /// </summary>
    /// <param name="request">The <see cref="LoginRequest"/> to map.</param>
    /// <returns>
    /// The mapped <see cref="LoginCommand"/>.
    /// </returns>
    public static partial LoginCommand ToCommand(this LoginRequest request);

    /// <summary>
    /// Maps the <see cref="RefreshRequest"/> instance to a <see cref="RefreshCommand"/> object.
    /// </summary>
    /// <param name="request">The <see cref="RefreshRequest"/> to map.</param>
    /// <returns>
    /// The mapped <see cref="RefreshCommand"/>.
    /// </returns>
    public static partial RefreshCommand ToCommand(this RefreshRequest request);

    /// <summary>
    /// Maps the <see cref="TokenPair"/> instance to a <see cref="TokenPairResponse"/> object.
    /// </summary>
    /// <param name="tokenPair">The <see cref="TokenPair"/> to map.</param>
    /// <returns>
    /// The mapped <see cref="TokenPairResponse"/>.
    /// </returns>
    public static partial TokenPairResponse ToResponse(this TokenPair tokenPair);
}