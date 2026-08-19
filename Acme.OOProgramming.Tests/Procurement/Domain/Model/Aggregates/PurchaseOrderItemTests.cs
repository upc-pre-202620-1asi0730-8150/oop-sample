using Acme.OOProgramming.Procurement.Domain.Model.Aggregates;
using Acme.OOProgramming.Procurement.Domain.Model.ValueObjects;
using Acme.OOProgramming.Shared.Domain.Model.ValueObjects;

namespace Acme.OOProgramming.Tests.Procurement.Domain.Model.Aggregates;

public class PurchaseOrderItemTests
{
    private readonly ProductId _productId = ProductId.New();
    private readonly Money _unitPrice = new(25.50m, "USD");

    [Fact]
    public void Constructor_WithValidArguments_InitializesSuccessfully()
    {
        var item = new PurchaseOrderItem(_productId, 3, _unitPrice);

        Assert.Equal(_productId, item.ProductId);
        Assert.Equal(3, item.Quantity);
        Assert.Equal(_unitPrice, item.UnitPrice);
    }

    [Fact]
    public void Constructor_WithDefaultProductId_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new PurchaseOrderItem(default, 3, _unitPrice));
    }

    [Fact]
    public void Constructor_WithDefaultUnitPrice_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new PurchaseOrderItem(_productId, 3, default));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void Constructor_WithZeroOrNegativeQuantity_ThrowsArgumentOutOfRangeException(int quantity)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new PurchaseOrderItem(_productId, quantity, _unitPrice));
    }

    [Fact]
    public void IncreaseQuantity_WithPositiveAmount_IncreasesQuantity()
    {
        var item = new PurchaseOrderItem(_productId, 2, _unitPrice);

        item.IncreaseQuantity(5);

        Assert.Equal(7, item.Quantity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void IncreaseQuantity_WithZeroOrNegativeAmount_ThrowsArgumentOutOfRangeException(int addition)
    {
        var item = new PurchaseOrderItem(_productId, 2, _unitPrice);

        Assert.Throws<ArgumentOutOfRangeException>(() => item.IncreaseQuantity(addition));
    }

    [Fact]
    public void CalculateItemTotal_ReturnsQuantityMultipliedByUnitPrice()
    {
        var item = new PurchaseOrderItem(_productId, 4, new Money(12.50m, "USD"));

        var total = item.CalculateItemTotal();

        Assert.Equal(50.00m, total.Amount);
        Assert.Equal("USD", total.Currency.Code);
    }
}
