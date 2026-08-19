using Acme.OOProgramming.Procurement.Domain.Model.ValueObjects;

namespace Acme.OOProgramming.Tests.Procurement.Domain.Model.ValueObjects;

public class ProductIdTests
{
    [Fact]
    public void ParameterlessConstructor_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => new ProductId());
    }

    [Fact]
    public void Constructor_WithValidGuid_InitializesSuccessfully()
    {
        var guid = Guid.NewGuid();
        var productId = new ProductId(guid);

        Assert.Equal(guid, productId.Id);
        Assert.Equal(guid.ToString(), productId.ToString());
    }

    [Fact]
    public void Constructor_WithEmptyGuid_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new ProductId(Guid.Empty));
    }

    [Fact]
    public void New_GeneratesVersion7Guid()
    {
        var productId = ProductId.New();

        Assert.NotEqual(Guid.Empty, productId.Id);
        Assert.Equal(7, productId.Id.Version);
    }

    [Fact]
    public void Equality_SameGuid_AreEqual()
    {
        var guid = Guid.NewGuid();
        var p1 = new ProductId(guid);
        var p2 = new ProductId(guid);
        var p3 = ProductId.New();

        Assert.Equal(p1, p2);
        Assert.True(p1 == p2);
        Assert.NotEqual(p1, p3);
        Assert.True(p1 != p3);
    }
}
