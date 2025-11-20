using Aidelythe.Api._Common.Locality;
using Aidelythe.Api.Organizing.Events.Requests;
using Aidelythe.Domain.Organizing.Events.ValueObjects;

namespace Aidelythe.Api.Organizing.Events.Validators;

/// <summary>
/// Represents a validator for event creation requests.
/// </summary>
public sealed class CreateEventRequestValidator : AbstractValidator<CreateEventRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateEventRequestValidator"/> class.
    /// </summary>
    public CreateEventRequestValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty()
            .MaximumLength(EventTitle.MaximumLength);

        RuleFor(request => request.Description)
            .MaximumLength(EventDescription.MaximumLength)
            .When(request => request.Description is not null);

        RuleFor(request => request.StartsAt)
            .NotNull();

        RuleFor(request => request.Location)
            .NotNull()
            .SetValidator(new AddressRequestValidator()!);
    }
}