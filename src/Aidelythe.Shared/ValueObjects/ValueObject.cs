namespace Aidelythe.Shared.ValueObjects;

/// <summary>
/// Represents a base value object.
/// </summary>
public abstract record ValueObject<T>
{
    /// <summary>
    /// Gets the encapsulated value of the value object.
    /// </summary>
    public T Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueObject{T}"/> class.
    /// </summary>
    /// <param name="value">The encapsulated value of the value object.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="value"/> is null.</exception>
    protected ValueObject(T value)
    {
        ThrowIfNull(value);

        Value = value;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Value}";
    }
}