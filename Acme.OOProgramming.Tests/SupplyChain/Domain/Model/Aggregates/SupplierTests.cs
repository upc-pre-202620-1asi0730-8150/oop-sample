using Acme.OOProgramming.Shared.Domain.Model.ValueObjects;
using Acme.OOProgramming.SupplyChain.Domain.Model.Aggregates;
using Acme.OOProgramming.SupplyChain.Domain.Model.ValueObjects;

namespace Acme.OOProgramming.Tests.SupplyChain.Domain.Model.Aggregates;

/// <summary>
/// Contains unit tests for the <see cref="Supplier"/> aggregate root.
/// </summary>
public class SupplierTests
{
    private readonly SupplierId _validSupplierId = new("SUP001");
    private readonly Address _validAddress = new("Main St", "100", "Springfield", "IL", "62701", "USA");

    /// <summary>
    /// Verifies that constructing a <see cref="Supplier"/> with a strongly-typed <see cref="SupplierId"/> initializes properties correctly.
    /// </summary>
    [Fact]
    public void Constructor_WithSupplierId_InitializesSuccessfully()
    {
        var supplier = new Supplier(_validSupplierId, "Acme Supplies", _validAddress);

        Assert.Equal(_validSupplierId, supplier.Id);
        Assert.Equal("Acme Supplies", supplier.Name);
        Assert.Equal(_validAddress, supplier.Address);
    }

    /// <summary>
    /// Verifies that constructing a <see cref="Supplier"/> with a string identifier initializes properties correctly.
    /// </summary>
    [Fact]
    public void Constructor_WithStringIdentifier_InitializesSuccessfully()
    {
        var supplier = new Supplier("SUP001", "Acme Supplies", _validAddress);

        Assert.Equal(_validSupplierId, supplier.Id);
        Assert.Equal("Acme Supplies", supplier.Name);
        Assert.Equal(_validAddress, supplier.Address);
    }

    /// <summary>
    /// Verifies that constructing a <see cref="Supplier"/> with null, empty, or whitespace name throws an <see cref="ArgumentException"/>.
    /// </summary>
    /// <param name="name">The invalid supplier name.</param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespaceName_ThrowsArgumentException(string? name)
    {
        Assert.ThrowsAny<ArgumentException>(() => new Supplier(_validSupplierId, name!, _validAddress));
    }

    /// <summary>
    /// Verifies that constructing a <see cref="Supplier"/> with a default uninitialized <see cref="Address"/> throws an <see cref="ArgumentException"/>.
    /// </summary>
    [Fact]
    public void Constructor_WithDefaultAddress_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Supplier(_validSupplierId, "Acme Supplies", default(Address)));
    }
}
