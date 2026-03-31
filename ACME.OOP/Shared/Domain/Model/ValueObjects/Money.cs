namespace ACME.OOP.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Represents a monetary value object. 
/// </summary>
public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    /// <summary>
    /// Creates a new instance of <see cref="Money"/>. 
    /// </summary>
    /// <param name="amount">The monetary amount.</param>
    /// <param name="currency">The currency.</param>
    /// <exception cref="ArgumentException">Thrown when the currency is not a valid 3-letter ISO code.</exception>
    public Money(decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency) || currency.Length  != 3)
            throw new ArgumentException("Currency must be a valid 3-letter ISO code.", nameof(currency));
        Amount = amount;
        Currency = currency;
    }
    /// <summary>
    /// Returns a string representation of the monetary value. 
    /// </summary>
    /// <returns>A string in the format "Amount Currency".</returns>
    public override string ToString() => $"{Amount} {Currency}";

    /// <summary>
    /// Adds two <see cref="Money"/> objects together.
    /// </summary>
    /// <param name="other">The other <see cref="Money"/> object to add. If null, the original <see cref="Money"/> object is returned.</param>
    /// <returns>A new <see cref="Money"/> object representing the sum of the two monetary values.</returns>
    public Money Add(Money? other)
    {
        return other == null ? this : new Money(Amount + other.Amount, Currency);
    }
    
    /// <summary>
    /// Multiplies the monetary value by a factor.
    /// </summary>
    /// <param name="factor">The factor to multiply the monetary value by.</param>
    /// <returns>A new <see cref="Money"/> object representing the result of the multiplication.</returns>
    public Money Multiply(int factor) => new(Amount * factor, Currency);
}