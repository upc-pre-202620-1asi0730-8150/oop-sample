using Acme.OOProgramming.SupplyChain.Domain.Model.ValueObjects;

namespace Acme.OOProgramming.Tests.SupplyChain.Domain.Model.ValueObjects;

/// <summary>
/// Contains unit tests for the <see cref="SupplierId"/> value object.
/// </summary>
public class SupplierIdTests
{
    /// <summary>
    /// Verifies that calling the parameterless constructor throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    [Fact]
    public void ParameterlessConstructor_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => new SupplierId());
    }

    /// <summary>
    /// Verifies that constructing a <see cref="SupplierId"/> with a valid identifier sets properties correctly.
    /// </summary>
    /// <param name="identifier">The test supplier identifier string.</param>
    [Theory]
    [InlineData("SUP001")]
    [InlineData("VEND-123")]
    [InlineData("ACME-CORP")]
    public void Constructor_WithValidIdentifier_InitializesSuccessfully(string identifier)
    {
        var supplierId = new SupplierId(identifier);

        Assert.Equal(identifier, supplierId.Identifier);
        Assert.Equal(identifier, supplierId.ToString());
    }

    /// <summary>
    /// Verifies that constructing a <see cref="SupplierId"/> with null, empty, or whitespace throws an <see cref="ArgumentException"/>.
    /// </summary>
    /// <param name="identifier">The invalid supplier identifier value.</param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespaceIdentifier_ThrowsArgumentException(string? identifier)
    {
        Assert.ThrowsAny<ArgumentException>(() => new SupplierId(identifier!));
    }

    /// <summary>
    /// Verifies that an uninitialized default <see cref="SupplierId"/> struct returns an empty string for <see cref="SupplierId.Identifier"/>.
    /// </summary>
    [Fact]
    public void DefaultStruct_IdentifierReturnsEmptyString()
    {
        SupplierId defaultId = default;

        Assert.Equal(string.Empty, defaultId.Identifier);
    }

    /// <summary>
    /// Verifies structural value equality for <see cref="SupplierId"/> instances.
    /// </summary>
    [Fact]
    public void Equality_SameIdentifier_AreEqual()
    {
        var id1 = new SupplierId("SUP001");
        var id2 = new SupplierId("SUP001");
        var id3 = new SupplierId("SUP002");

        Assert.Equal(id1, id2);
        Assert.True(id1 == id2);
        Assert.NotEqual(id1, id3);
        Assert.True(id1 != id3);
    }
}
