using Acme.OOProgramming.Shared.Domain.Model.ValueObjects;

namespace Acme.OOProgramming.Tests.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Contains unit tests for the <see cref="Address"/> value object.
/// </summary>
public class AddressTests
{
    /// <summary>
    /// Verifies that calling the parameterless constructor throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    [Fact]
    public void ParameterlessConstructor_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => new Address());
    }

    /// <summary>
    /// Verifies that constructing an <see cref="Address"/> with valid components initializes properties correctly.
    /// </summary>
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

    /// <summary>
    /// Verifies that constructing an <see cref="Address"/> with a null state or region initializes successfully and formats correctly.
    /// </summary>
    [Fact]
    public void Constructor_WithNullStateOrRegion_InitializesSuccessfully()
    {
        var address = new Address("Main St", "100", "Springfield", null, "62701", "USA");

        Assert.Null(address.StateOrRegion);
        Assert.Equal("Main St, 100, Springfield, 62701, USA", address.ToString());
    }

    /// <summary>
    /// Verifies that constructing an <see cref="Address"/> with null, empty, or whitespace street throws an <see cref="ArgumentException"/>.
    /// </summary>
    /// <param name="street">The invalid street value.</param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespaceStreet_ThrowsArgumentException(string? street)
    {
        Assert.ThrowsAny<ArgumentException>(() => new Address(street!, "100", "City", null, "12345", "Country"));
    }

    /// <summary>
    /// Verifies that constructing an <see cref="Address"/> with a street exceeding 100 characters throws an <see cref="ArgumentException"/>.
    /// </summary>
    [Fact]
    public void Constructor_WithStreetExceeding100Chars_ThrowsArgumentException()
    {
        var longStreet = new string('a', 101);
        Assert.Throws<ArgumentException>(() => new Address(longStreet, "100", "City", null, "12345", "Country"));
    }

    /// <summary>
    /// Verifies that constructing an <see cref="Address"/> with null, empty, or whitespace number throws an <see cref="ArgumentException"/>.
    /// </summary>
    /// <param name="number">The invalid street number value.</param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespaceNumber_ThrowsArgumentException(string? number)
    {
        Assert.ThrowsAny<ArgumentException>(() => new Address("Street", number!, "City", null, "12345", "Country"));
    }

    /// <summary>
    /// Verifies that constructing an <see cref="Address"/> with null, empty, or whitespace city throws an <see cref="ArgumentException"/>.
    /// </summary>
    /// <param name="city">The invalid city value.</param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespaceCity_ThrowsArgumentException(string? city)
    {
        Assert.ThrowsAny<ArgumentException>(() => new Address("Street", "100", city!, null, "12345", "Country"));
    }

    /// <summary>
    /// Verifies that constructing an <see cref="Address"/> with null, empty, or whitespace postal code throws an <see cref="ArgumentException"/>.
    /// </summary>
    /// <param name="postalCode">The invalid postal code value.</param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespacePostalCode_ThrowsArgumentException(string? postalCode)
    {
        Assert.ThrowsAny<ArgumentException>(() => new Address("Street", "100", "City", null, postalCode!, "Country"));
    }

    /// <summary>
    /// Verifies that constructing an <see cref="Address"/> with null, empty, or whitespace country throws an <see cref="ArgumentException"/>.
    /// </summary>
    /// <param name="country">The invalid country value.</param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespaceCountry_ThrowsArgumentException(string? country)
    {
        Assert.ThrowsAny<ArgumentException>(() => new Address("Street", "100", "City", null, "12345", country!));
    }

    /// <summary>
    /// Verifies that an uninitialized default <see cref="Address"/> struct returns empty strings for string properties.
    /// </summary>
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

    /// <summary>
    /// Verifies that <see cref="Address.ToString"/> formats the complete address including state/region properly.
    /// </summary>
    [Fact]
    public void ToString_WithStateOrRegion_FormatsCorrectly()
    {
        var address = new Address("Main St", "100", "Springfield", "IL", "62701", "USA");

        Assert.Equal("Main St, 100, Springfield, IL, 62701, USA", address.ToString());
    }

    /// <summary>
    /// Verifies value equality across <see cref="Address"/> instances.
    /// </summary>
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
