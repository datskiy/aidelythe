using Aidelythe.Api.Organizing.Requests;
using Aidelythe.Domain.Organizing.ValueObjects;

namespace Aidelythe.Api.Organizing.Validators;

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
    }
}