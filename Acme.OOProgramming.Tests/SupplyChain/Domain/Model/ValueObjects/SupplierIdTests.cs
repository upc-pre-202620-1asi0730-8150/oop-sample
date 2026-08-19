using Acme.OOProgramming.SupplyChain.Domain.Model.ValueObjects;

namespace Acme.OOProgramming.Tests.SupplyChain.Domain.Model.ValueObjects;

public class SupplierIdTests
{
    [Fact]
    public void ParameterlessConstructor_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => new SupplierId());
    }

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

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespaceIdentifier_ThrowsArgumentException(string? identifier)
    {
        Assert.ThrowsAny<ArgumentException>(() => new SupplierId(identifier!));
    }

    [Fact]
    public void DefaultStruct_IdentifierReturnsEmptyString()
    {
        SupplierId defaultId = default;

        Assert.Equal(string.Empty, defaultId.Identifier);
    }

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
