using Aidelythe.Api._System.Authentication.Requests;
using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Api._System.Authentication.Validators;

/// <summary>
/// Represents a validator for registration requests.
/// </summary>
public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginRequestValidator"/> class.
    /// </summary>
    public LoginRequestValidator()
    {
        RuleFor(request => request.Login)
            .NotEmpty()
            .MaximumLength(Email.MaximumLength);

        RuleFor(request => request.Password)
            .NotEmpty()
            .Length(Password.MinimumLength, Password.MaximumLength);
    }
}