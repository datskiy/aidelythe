using Aidelythe.Api._Common.Validation;
using Aidelythe.Api._System.Authentication.Requests;
using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Api._System.Authentication.Validators;

/// <summary>
/// Represents a validator for registration requests.
/// </summary>
public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterRequestValidator"/> class.
    /// </summary>
    public RegisterRequestValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .MaximumLength(Email.MaximumLength)
            .Matches(Email.FormatRegex)
            .WhenNotNull();

        RuleFor(request => request.PhoneNumber)
            .NotEmpty()
            .Matches(PhoneNumber.FormatRegex)
            .WhenNotNull();

        RuleFor(request => request.Password)
            .Length(Password.MinimumLength, Password.MaximumLength)
;
    }
}