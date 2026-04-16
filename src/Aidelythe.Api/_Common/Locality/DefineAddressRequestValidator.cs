using Aidelythe.Api._Common.Validation;
using Aidelythe.Domain._Common.Locality;

namespace Aidelythe.Api._Common.Locality;

/// <summary>
/// Represents a validator for address definition requests.
/// </summary>
public sealed class DefineAddressRequestValidator : AbstractValidator<DefineAddressRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DefineAddressRequestValidator"/> class.
    /// </summary>
    public DefineAddressRequestValidator()
    {
        RuleFor(request => request.Country)
            .NotEmpty()
            .MaximumLength(Address.MaximumAreaNameLength);

        RuleFor(request => request.Region)
            .NotEmpty()
            .MaximumLength(Address.MaximumAreaNameLength)
            .WhenNotNull();

        RuleFor(request => request.City)
            .NotEmpty()
            .MaximumLength(Address.MaximumAreaNameLength)
            .WhenNotNull();

        RuleFor(request => request.PostalCode)
            .NotEmpty()
            .MaximumLength(Address.MaximumPostalCodeLength)
            .Matches(Address.PostalCodeFormatRegex)
            .WhenNotNull();

        RuleFor(request => request.Street)
            .NotEmpty()
            .MaximumLength(Address.MaximumStreetNameLength)
            .WhenNotNull();
    }
}