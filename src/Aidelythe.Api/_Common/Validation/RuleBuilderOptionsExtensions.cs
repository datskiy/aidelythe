namespace Aidelythe.Api._Common.Validation;

/// <summary>
/// Provides extension methods for configuring validation rules.
/// </summary>
public static class RuleBuilderOptionsExtensions
{
    /// <summary>
    /// Specifies a condition which ensures that the validation logic is only executed
    /// when the property being validated is not null.
    /// </summary>
    /// <param name="rule">The rule to which the condition is applied.</param>
    /// <typeparam name="T">The type of the object being validated.</typeparam>
    /// <typeparam name="TProperty">The type of the property being validated.</typeparam>
    /// <returns>
    /// The <see cref="IRuleBuilderOptions{T, TProperty}"/> with the condition applied.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="rule"/> is null.</exception>
    public static IRuleBuilderOptions<T, TProperty> WhenNotNull<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule)
    {
        ThrowIfNull(rule);

        return rule.Configure(cfg =>
        {
            cfg.ApplyCondition(context =>
            {
                var propertyValue = cfg.GetPropertyValue(context.InstanceToValidate);
                return propertyValue is not null;
            });
        });
    }
}