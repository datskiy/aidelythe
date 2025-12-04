using Aidelythe.Api._Common.Http.Responses;
using Aidelythe.Shared.Collections;

namespace Aidelythe.Api._System.Validation;

/// <summary>
/// Represents an action filter that performs validation for bound instances.
/// </summary>
public sealed class ValidationFilter : IAsyncActionFilter
{
    /// <summary>
    /// Validates the bound instance using the corresponding registered validator.
    /// If validation fails, the action result is set to <see cref="BadRequestObjectResult"/>.
    /// </summary>
    /// <remarks>
    /// Validation is considered required for a reference-type instance bound to the action parameters.
    /// </remarks>
    /// <param name="context">The context in which the action is executed.</param>
    /// <param name="next">
    /// The delegate that executes the action method and filters after this one in the pipeline.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="context"/> or <paramref name="next"/> is null.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// A validator for the specified instance type is not registered.
    /// </exception>
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        ThrowIfNull(context);
        ThrowIfNull(next);

        var isValidationRequired = context.ActionDescriptor.Parameters
            .SingleOrDefault(descriptor => descriptor.ParameterType.IsClass) is not null;

        if (!isValidationRequired)
        {
            await next();
            return;
        }

        var httpContext = context.HttpContext;
        var boundInstance = context.ActionArguments.Values
            .SingleOrDefault(arg => arg is not null && arg.GetType().IsClass);

        if (boundInstance is null)
        {
            context.Result = new BadRequestObjectResult(new BadRequestResponse(httpContext.TraceIdentifier));
            return;
        }

        var validatorType = typeof(IValidator<>).MakeGenericType(boundInstance.GetType());
        var validator = (IValidator)httpContext.RequestServices.GetRequiredService(validatorType);
        if (validator is null)
            throw new InvalidOperationException($"Validator for {boundInstance.GetType().Name} is not registered.");

        var validationContext = new ValidationContext<object>(boundInstance);
        var validationResult = await validator.ValidateAsync(validationContext, context.HttpContext.RequestAborted);
        if (!validationResult.IsValid)
        {
            context.Result = new BadRequestObjectResult(new BadRequestResponse(
                validationResult.Errors.AsNonEmpty(),
                httpContext.TraceIdentifier));

            return;
        }

        await next();
    }
}