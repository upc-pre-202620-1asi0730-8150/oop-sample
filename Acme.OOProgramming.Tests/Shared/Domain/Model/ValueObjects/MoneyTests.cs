using Acme.OOProgramming.Shared.Domain.Model.ValueObjects;

namespace Acme.OOProgramming.Tests.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Contains unit tests for the <see cref="Money"/> value object.
/// </summary>
public class MoneyTests
{
    /// <summary>
    /// Verifies that invoking the parameterless constructor throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    [Fact]
    public void ParameterlessConstructor_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => new Money());
    }

    /// <summary>
    /// Verifies that constructing a <see cref="Money"/> instance with valid amount and currency initializes successfully.
    /// </summary>
    [Fact]
    public void Constructor_WithValidAmountAndCurrency_InitializesSuccessfully()
    {
        var currency = new Currency("USD");
        var money = new Money(100.50m, currency);

        Assert.Equal(100.50m, money.Amount);
        Assert.Equal(currency, money.Currency);
        Assert.Equal("100.50 USD", money.ToString());
    }

    /// <summary>
    /// Verifies that constructing a <see cref="Money"/> instance with a string currency code initializes successfully.
    /// </summary>
    [Fact]
    public void Constructor_WithStringCurrencyCode_InitializesSuccessfully()
    {
        var money = new Money(50.00m, "EUR");

        Assert.Equal(50.00m, money.Amount);
        Assert.Equal("EUR", money.Currency.Code);
    }

    /// <summary>
    /// Verifies that constructing a <see cref="Money"/> instance with a negative amount throws an <see cref="ArgumentOutOfRangeException"/>.
    /// </summary>
    [Fact]
    public void Constructor_WithNegativeAmount_ThrowsArgumentOutOfRangeException()
    {
        var currency = new Currency("USD");
        Assert.Throws<ArgumentOutOfRangeException>(() => new Money(-1.00m, currency));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Money(-1.00m, "USD"));
    }

    /// <summary>
    /// Verifies that constructing a <see cref="Money"/> instance with a default currency throws an <see cref="ArgumentException"/>.
    /// </summary>
    [Fact]
    public void Constructor_WithDefaultCurrency_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Money(10.00m, default(Currency)));
    }

    /// <summary>
    /// Verifies that adding two <see cref="Money"/> values with the same currency returns their combined sum.
    /// </summary>
    [Fact]
    public void Add_WithSameCurrency_ReturnsCombinedSum()
    {
        var m1 = new Money(10.50m, "USD");
        var m2 = new Money(20.25m, "USD");

        var sumMethod = m1.Add(m2);
        var sumOp = m1 + m2;

        Assert.Equal(30.75m, sumMethod.Amount);
        Assert.Equal("USD", sumMethod.Currency.Code);
        Assert.Equal(sumMethod, sumOp);
    }

    /// <summary>
    /// Verifies that adding two <see cref="Money"/> values with different currencies throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    [Fact]
    public void Add_WithDifferentCurrency_ThrowsInvalidOperationException()
    {
        var m1 = new Money(10.00m, "USD");
        var m2 = new Money(10.00m, "EUR");

        Assert.Throws<InvalidOperationException>(() => m1.Add(m2));
        Assert.Throws<InvalidOperationException>(() => m1 + m2);
    }

    /// <summary>
    /// Verifies that adding an uninitialized default <see cref="Money"/> throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    [Fact]
    public void Add_WithDefaultMoney_ThrowsInvalidOperationException()
    {
        var m1 = new Money(10.00m, "USD");
        Money defaultMoney = default;

        Assert.Throws<InvalidOperationException>(() => m1.Add(defaultMoney));
        Assert.Throws<InvalidOperationException>(() => defaultMoney.Add(m1));
        Assert.Throws<InvalidOperationException>(() => m1 + defaultMoney);
        Assert.Throws<InvalidOperationException>(() => defaultMoney + m1);
    }

    /// <summary>
    /// Verifies that multiplying a <see cref="Money"/> instance by a factor calculates the correct result.
    /// </summary>
    [Fact]
    public void Multiply_WithValidFactor_ReturnsMultipliedAmount()
    {
        var money = new Money(25.00m, "USD");

        var resultMethod = money.Multiply(4m);
        var resultOp1 = money * 4m;
        var resultOp2 = 4m * money;

        Assert.Equal(100.00m, resultMethod.Amount);
        Assert.Equal("USD", resultMethod.Currency.Code);
        Assert.Equal(resultMethod, resultOp1);
        Assert.Equal(resultMethod, resultOp2);
    }

    /// <summary>
    /// Verifies that multiplying a <see cref="Money"/> instance by a negative factor throws an <see cref="ArgumentOutOfRangeException"/>.
    /// </summary>
    [Fact]
    public void Multiply_WithNegativeFactor_ThrowsArgumentOutOfRangeException()
    {
        var money = new Money(25.00m, "USD");

        Assert.Throws<ArgumentOutOfRangeException>(() => money.Multiply(-2m));
        Assert.Throws<ArgumentOutOfRangeException>(() => money * -2m);
        Assert.Throws<ArgumentOutOfRangeException>(() => -2m * money);
    }

    /// <summary>
    /// Verifies that multiplying an uninitialized default <see cref="Money"/> instance throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    [Fact]
    public void Multiply_WithDefaultMoney_ThrowsInvalidOperationException()
    {
        Money defaultMoney = default;

        Assert.Throws<InvalidOperationException>(() => defaultMoney.Multiply(2m));
        Assert.Throws<InvalidOperationException>(() => defaultMoney * 2m);
        Assert.Throws<InvalidOperationException>(() => 2m * defaultMoney);
    }

    /// <summary>
    /// Verifies value equality across <see cref="Money"/> instances.
    /// </summary>
    [Fact]
    public void Equality_SameAmountAndCurrency_AreEqual()
    {
        var m1 = new Money(15.99m, "USD");
        var m2 = new Money(15.99m, "USD");
        var m3 = new Money(20.00m, "USD");
        var m4 = new Money(15.99m, "EUR");

        Assert.Equal(m1, m2);
        Assert.True(m1 == m2);
        Assert.NotEqual(m1, m3);
        Assert.NotEqual(m1, m4);
    }
}
