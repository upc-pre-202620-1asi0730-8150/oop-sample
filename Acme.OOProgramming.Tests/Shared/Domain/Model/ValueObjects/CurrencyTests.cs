using Acme.OOProgramming.Shared.Domain.Model.ValueObjects;

namespace Acme.OOProgramming.Tests.Shared.Domain.Model.ValueObjects;

public class CurrencyTests
{
    [Fact]
    public void ParameterlessConstructor_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => new Currency());
    }

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

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespaceCode_ThrowsArgumentException(string? code)
    {
        Assert.ThrowsAny<ArgumentException>(() => new Currency(code!));
    }

    [Theory]
    [InlineData("US")]
    [InlineData("USDD")]
    [InlineData("U")]
    public void Constructor_WithInvalidLength_ThrowsArgumentException(string code)
    {
        Assert.Throws<ArgumentException>(() => new Currency(code));
    }

    [Theory]
    [InlineData("U1D")]
    [InlineData("US$")]
    [InlineData("123")]
    public void Constructor_WithNonAlphabeticCharacters_ThrowsArgumentException(string code)
    {
        Assert.Throws<ArgumentException>(() => new Currency(code));
    }

    [Fact]
    public void DefaultStruct_CodeReturnsEmptyString()
    {
        Currency defaultCurrency = default;

        Assert.Equal(string.Empty, defaultCurrency.Code);
    }

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
