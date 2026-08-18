namespace ACME.OOP.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Represents a currency value object.
/// </summary>
public readonly record struct Currency
{
    /// <summary>
    /// The currency code.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the currency code is null, empty, or not a valid 3-letter ISO 4217 alphabetic code.</exception>
    public string Code
    {
        get;
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length != 3 || !value.All(char.IsAsciiLetter))
                throw new ArgumentException("Currency must be a valid 3-letter ISO 4217 alphabetic code.", nameof(Code));
            field = value.ToUpperInvariant();
        }
    }
    
    /// <summary>
    /// Creates a new instance of <see cref="Currency"/>.
    /// </summary>
    /// <param name="code">The currency code.</param>
    /// <exception cref="ArgumentException">Thrown when the currency code is null, empty, or not a valid 3-letter ISO 4217 alphabetic code.</exception>
    public Currency(string code) => Code = code;
    
    /// <summary>
    /// Returns a string representation of the currency code.
    /// </summary>
    /// <returns>A string representation of the currency code.</returns>
    public override string ToString() => Code;
}