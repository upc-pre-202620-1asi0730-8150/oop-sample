using Acme.OOProgramming.Shared.Domain.Model.ValueObjects;

namespace Acme.OOProgramming.Tests.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Contains unit tests for the <see cref="Currency"/> value object.
/// </summary>
public class CurrencyTests
{
    /// <summary>
    /// Verifies that calling the parameterless constructor throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    [Fact]
    public void ParameterlessConstructor_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => new Currency());
    }

    /// <summary>
    /// Verifies that constructing a <see cref="Currency"/> with valid ISO 4217 codes succeeds.
    /// </summary>
    /// <param name="code">The valid currency code.</param>
    [Theory]
    [InlineData("USD")]
    [InlineData("EUR")]
    [InlineData("PEN")]
    [InlineData("GBP")]
    public void Constructor_WithValidCode_InitializesSuccessfully(string code)
    {
        var currency = new Currency(code);

        Assert.Equal(code, currency.Code);
        Assert.Equal(code, currency.ToString());
    }

    /// <summary>
    /// Verifies that constructing a <see cref="Currency"/> with null or whitespace throws an <see cref="ArgumentException"/>.
    /// </summary>
    /// <param name="code">The invalid null or whitespace currency code.</param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespaceCode_ThrowsArgumentException(string? code)
    {
        Assert.ThrowsAny<ArgumentException>(() => new Currency(code!));
    }

    /// <summary>
    /// Verifies that currency codes with invalid length throw an <see cref="ArgumentException"/>.
    /// </summary>
    /// <param name="code">The invalid length currency code.</param>
    [Theory]
    [InlineData("US")]
    [InlineData("USDD")]
    [InlineData("U")]
    public void Constructor_WithInvalidLength_ThrowsArgumentException(string code)
    {
        Assert.Throws<ArgumentException>(() => new Currency(code));
    }

    /// <summary>
    /// Verifies that currency codes containing non-alphabetic characters throw an <see cref="ArgumentException"/>.
    /// </summary>
    /// <param name="code">The invalid non-alphabetic code.</param>
    [Theory]
    [InlineData("U1D")]
    [InlineData("US$")]
    [InlineData("123")]
    public void Constructor_WithNonAlphabeticCharacters_ThrowsArgumentException(string code)
    {
        Assert.Throws<ArgumentException>(() => new Currency(code));
    }

    /// <summary>
    /// Verifies that an uninitialized default <see cref="Currency"/> struct returns an empty string for <see cref="Currency.Code"/>.
    /// </summary>
    [Fact]
    public void DefaultStruct_CodeReturnsEmptyString()
    {
        Currency defaultCurrency = default;

        Assert.Equal(string.Empty, defaultCurrency.Code);
    }

    /// <summary>
    /// Verifies value equality across <see cref="Currency"/> instances.
    /// </summary>
    [Fact]
    public void Equality_SameCodes_AreEqual()
    {
        var c1 = new Currency("USD");
        var c2 = new Currency("USD");
        var c3 = new Currency("EUR");

        Assert.Equal(c1, c2);
        Assert.True(c1 == c2);
        Assert.NotEqual(c1, c3);
        Assert.True(c1 != c3);
    }
}
