using Acme.OOProgramming.Shared.Domain.Model.ValueObjects;

namespace Acme.OOProgramming.Tests.Shared.Domain.Model.ValueObjects;

public class AddressTests
{
    [Fact]
    public void ParameterlessConstructor_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => new Address());
    }

    [Fact]
    public void Constructor_WithValidComponents_InitializesSuccessfully()
    {
        var address = new Address("Main St", "100", "Springfield", "IL", "62701", "USA");

        Assert.Equal("Main St", address.Street);
        Assert.Equal("100", address.Number);
        Assert.Equal("Springfield", address.City);
        Assert.Equal("IL", address.StateOrRegion);
        Assert.Equal("62701", address.PostalCode);
        Assert.Equal("USA", address.Country);
    }

    [Fact]
    public void Constructor_WithNullStateOrRegion_InitializesSuccessfully()
    {
        var address = new Address("Main St", "100", "Springfield", null, "62701", "USA");

        Assert.Null(address.StateOrRegion);
        Assert.Equal("Main St, 100, Springfield, 62701, USA", address.ToString());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespaceStreet_ThrowsArgumentException(string? street)
    {
        Assert.ThrowsAny<ArgumentException>(() => new Address(street!, "100", "City", null, "12345", "Country"));
    }

    [Fact]
    public void Constructor_WithStreetExceeding100Chars_ThrowsArgumentException()
    {
        var longStreet = new string('a', 101);
        Assert.Throws<ArgumentException>(() => new Address(longStreet, "100", "City", null, "12345", "Country"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespaceNumber_ThrowsArgumentException(string? number)
    {
        Assert.ThrowsAny<ArgumentException>(() => new Address("Street", number!, "City", null, "12345", "Country"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespaceCity_ThrowsArgumentException(string? city)
    {
        Assert.ThrowsAny<ArgumentException>(() => new Address("Street", "100", city!, null, "12345", "Country"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespacePostalCode_ThrowsArgumentException(string? postalCode)
    {
        Assert.ThrowsAny<ArgumentException>(() => new Address("Street", "100", "City", null, postalCode!, "Country"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespaceCountry_ThrowsArgumentException(string? country)
    {
        Assert.ThrowsAny<ArgumentException>(() => new Address("Street", "100", "City", null, "12345", country!));
    }

    [Fact]
    public void DefaultStruct_PropertiesReturnEmptyStrings()
    {
        Address defaultAddress = default;

        Assert.Equal(string.Empty, defaultAddress.Street);
        Assert.Equal(string.Empty, defaultAddress.Number);
        Assert.Equal(string.Empty, defaultAddress.City);
        Assert.Equal(string.Empty, defaultAddress.PostalCode);
        Assert.Equal(string.Empty, defaultAddress.Country);
    }

    [Fact]
    public void ToString_WithStateOrRegion_FormatsCorrectly()
    {
        var address = new Address("Main St", "100", "Springfield", "IL", "62701", "USA");

        Assert.Equal("Main St, 100, Springfield, IL, 62701, USA", address.ToString());
    }

    [Fact]
    public void Equality_SameValues_AreEqual()
    {
        var a1 = new Address("Main St", "100", "Springfield", "IL", "62701", "USA");
        var a2 = new Address("Main St", "100", "Springfield", "IL", "62701", "USA");
        var a3 = new Address("Other St", "100", "Springfield", "IL", "62701", "USA");

        Assert.Equal(a1, a2);
        Assert.True(a1 == a2);
        Assert.NotEqual(a1, a3);
        Assert.True(a1 != a3);
    }
}
