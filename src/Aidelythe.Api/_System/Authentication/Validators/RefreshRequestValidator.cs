using Aidelythe.Api._System.Authentication.Requests;
using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Api._System.Authentication.Validators;

/// <summary>
/// Represents a validator for refresh requests.
/// </summary>
public sealed class RefreshRequestValidator : AbstractValidator<RefreshRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshRequestValidator"/> class.
    /// </summary>
    public RefreshRequestValidator()
    {
        RuleFor(request => request.RefreshToken)
            .NotEmpty()
            .MaximumLength(RefreshToken.MaximumLength);
    }
}