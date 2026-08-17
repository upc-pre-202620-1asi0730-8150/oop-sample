namespace ACME.OOP.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Represents a monetary value object. 
/// </summary>
public readonly record struct Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    /// <summary>
    /// Creates a new instance of <see cref="Money"/>. 
    /// </summary>
    /// <param name="amount">The monetary amount.</param>
    /// <param name="currency">The currency.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the amount is negative.</exception>
    /// <exception cref="ArgumentException">Thrown when the currency is not a valid 3-letter ISO code.</exception>
    public Money(decimal amount, string currency)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);
        ArgumentException.ThrowIfNullOrWhiteSpace(currency);
        if (currency.Length != 3)
            throw new ArgumentException("Currency must be a valid 3-letter ISO code.", nameof(currency));

        Amount = amount;
        Currency = currency.ToUpperInvariant();
    }

    /// <summary>
    /// Returns a string representation of the monetary value. 
    /// </summary>
    /// <returns>A string in the format "Amount Currency".</returns>
    public override string ToString() => $"{Amount} {Currency}";

    /// <summary>
    /// Adds two <see cref="Money"/> objects together.
    /// </summary>
    /// <param name="other">The other <see cref="Money"/> object to add.</param>
    /// <returns>A new <see cref="Money"/> object representing the sum of the two monetary values.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the currencies do not match.</exception>
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot add money with different currencies: '{Currency}' and '{other.Currency}'.");

        return new Money(Amount + other.Amount, Currency);
    }

    /// <summary>
    /// Multiplies the monetary value by an integer factor.
    /// </summary>
    /// <param name="factor">The factor to multiply the monetary value by.</param>
    /// <returns>A new <see cref="Money"/> object representing the result of the multiplication.</returns>
    public Money Multiply(int factor) => Multiply((decimal)factor);

    /// <summary>
    /// Multiplies the monetary value by a decimal factor.
    /// </summary>
    /// <param name="factor">The factor to multiply the monetary value by.</param>
    /// <returns>A new <see cref="Money"/> object representing the result of the multiplication.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when factor is negative.</exception>
    public Money Multiply(decimal factor)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(factor);
        return new Money(Amount * factor, Currency);
    }

    public static Money operator +(Money left, Money right) => left.Add(right);
    public static Money operator *(Money money, decimal factor) => money.Multiply(factor);
    public static Money operator *(decimal factor, Money money) => money.Multiply(factor);
    public static Money operator *(Money money, int factor) => money.Multiply(factor);
    public static Money operator *(int factor, Money money) => money.Multiply(factor);
}
