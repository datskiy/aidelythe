namespace Aidelythe.Api._Common.Http.Serialization;

/// <summary>
/// Provides helper methods for resolving JSON property names.
/// </summary>
public static class JsonPropertyNameHelper
{
    /// <summary>
    /// Attempts to resolve the JSON property name for the given member expression.
    /// </summary>
    /// <typeparam name="TType">The type containing the member.</typeparam>
    /// <param name="expression">
    /// The expression specifying the member for which the JSON property name is to be resolved.
    /// </param>
    /// <returns>
    /// The JSON property name if resolved successfully.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="expression"/> is null.</exception>
    /// <exception cref="ArgumentException">The expression does not represent valid member access.</exception>
    /// <exception cref="InvalidOperationException">
    /// Neither <see cref="JsonPropertyNameAttribute"/> nor <see cref="FromQueryAttribute"/>
    /// is specified for the given member.
    /// </exception>
    public static string TryResolve<TType>(Expression<Func<TType, object?>> expression)
    {
        ThrowIfNull(expression);

        var memberExpression = expression.Body switch
        {
            MemberExpression exp => exp,
            UnaryExpression { Operand: MemberExpression exp } => exp,
            _ => throw new ArgumentException("Expression must be a member access.", nameof(expression))
        };

        return TryResolve(memberExpression.Member);
    }

    /// <summary>
    /// Attempts to resolve the JSON property name for the specified member.
    /// </summary>
    /// <param name="memberInfo">
    /// The member info for which the JSON property name is to be resolved.
    /// </param>
    /// <returns>
    /// The JSON property name if resolved successfully.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="memberInfo"/> is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// Neither <see cref="JsonPropertyNameAttribute"/> nor <see cref="FromQueryAttribute"/>
    /// is specified for the given member.
    /// </exception>
    public static string TryResolve(MemberInfo memberInfo)
    {
        ThrowIfNull(memberInfo);

        var jsonPropertyName =
            memberInfo.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ??
            memberInfo.GetCustomAttribute<FromQueryAttribute>()?.Name;

        return jsonPropertyName ?? throw new InvalidOperationException(
            $"Neither JsonPropertyNameAttribute nor FromQueryAttribute is specified for {memberInfo.Name}.");
    }
}