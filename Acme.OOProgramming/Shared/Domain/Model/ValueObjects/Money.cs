namespace Acme.OOProgramming.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Represents a monetary value object. 
/// </summary>
public readonly record struct Money
{
    /// <summary>
    /// The underlying amount.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the amount is negative.</exception>
    public decimal Amount
    {
        get;
        init
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            field = value;
        }
    }

    /// <summary>
    /// The currency.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the currency is not a valid 3-letter ISO code.</exception>
    public Currency Currency
    {
        get;
        init
        {
            if (value == default)
                throw new ArgumentException("Currency is required.", nameof(Currency));
            field = value;
        }
    }

    /// <summary>
    /// Prevents parameterless construction of <see cref="Money"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Always thrown because an amount and currency are required.</exception>
    public Money() => throw new InvalidOperationException("Money must be initialized with an amount and currency.");

    /// <summary>
    /// Creates a new instance of <see cref="Money"/>. 
    /// </summary>
    /// <param name="amount">The monetary amount.</param>
    /// <param name="currency">The currency.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the amount is negative.</exception>
    /// <exception cref="ArgumentException">Thrown when the currency is not a valid 3-letter ISO code.</exception>
    public Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Money"/> with the specified amount and currency code.
    /// </summary>
    /// <param name="amount">The monetary amount.</param>
    /// <param name="currencyCode">The currency code.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the amount is negative.</exception>
    /// <exception cref="ArgumentException">Thrown when the currency code is not a valid 3-letter ISO code.</exception>
    public Money(decimal amount, string currencyCode) : this(amount, new Currency(currencyCode)) { }

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
    /// <exception cref="InvalidOperationException">Thrown when the currencies do not match or an instance is uninitialized.</exception>
    public Money Add(Money other)
    {
        if (Currency == default || other.Currency == default)
            throw new InvalidOperationException("Cannot perform arithmetic on uninitialized Money instances.");

        if (Currency != other.Currency)
            throw new InvalidOperationException(
                $"Cannot add money with different currencies: '{Currency}' and '{other.Currency}'.");

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
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the factor is negative.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the instance is uninitialized.</exception>
    public Money Multiply(decimal factor)
    {
        if (Currency == default)
            throw new InvalidOperationException("Cannot perform arithmetic on uninitialized Money instances.");

        ArgumentOutOfRangeException.ThrowIfNegative(factor);
        return new Money(Amount * factor, Currency);
    }

    /// <summary>
    /// Gets the result of adding two <see cref="Money"/> instances.
    /// </summary>
    /// <param name="left">The first monetary operand.</param>
    /// <param name="right">The second monetary operand.</param>
    /// <returns>The sum of the two monetary values.</returns>
    public static Money operator +(Money left, Money right) => left.Add(right);

    /// <summary>
    /// Multiplies a <see cref="Money"/> value by a decimal factor.
    /// </summary>
    /// <param name="money">The monetary value.</param>
    /// <param name="factor">The multiplier factor.</param>
    /// <returns>The multiplied monetary value.</returns>
    public static Money operator *(Money money, decimal factor) => money.Multiply(factor);

    /// <summary>
    /// Multiplies a <see cref="Money"/> value by a decimal factor.
    /// </summary>
    /// <param name="factor">The multiplier factor.</param>
    /// <param name="money">The monetary value.</param>
    /// <returns>The multiplied monetary value.</returns>
    public static Money operator *(decimal factor, Money money) => money.Multiply(factor);

    /// <summary>
    /// Multiplies a <see cref="Money"/> value by an integer factor.
    /// </summary>
    /// <param name="money">The monetary value.</param>
    /// <param name="factor">The multiplier factor.</param>
    /// <returns>The multiplied monetary value.</returns>
    public static Money operator *(Money money, int factor) => money.Multiply(factor);

    /// <summary>
    /// Multiplies a <see cref="Money"/> value by an integer factor.
    /// </summary>
    /// <param name="factor">The multiplier factor.</param>
    /// <param name="money">The monetary value.</param>
    /// <returns>The multiplied monetary value.</returns>
    public static Money operator *(int factor, Money money) => money.Multiply(factor);
}