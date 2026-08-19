using Acme.OOProgramming.Procurement.Domain.Model.Aggregates;
using Acme.OOProgramming.Procurement.Domain.Model.ValueObjects;
using Acme.OOProgramming.Shared.Domain.Model.ValueObjects;

namespace Acme.OOProgramming.Tests.Procurement.Domain.Model.Aggregates;

/// <summary>
/// Contains unit tests for the <see cref="PurchaseOrderItem"/> aggregate entity.
/// </summary>
public class PurchaseOrderItemTests
{
    private readonly ProductId _productId = ProductId.New();
    private readonly Money _unitPrice = new(25.50m, "USD");

    /// <summary>
    /// Verifies that constructing a <see cref="PurchaseOrderItem"/> with valid arguments sets properties accurately.
    /// </summary>
    [Fact]
    public void Constructor_WithValidArguments_InitializesSuccessfully()
    {
        var item = new PurchaseOrderItem(_productId, 3, _unitPrice);

        Assert.Equal(_productId, item.ProductId);
        Assert.Equal(3, item.Quantity);
        Assert.Equal(_unitPrice, item.UnitPrice);
    }

    /// <summary>
    /// Verifies that constructing a <see cref="PurchaseOrderItem"/> with a default <see cref="ProductId"/> throws an <see cref="ArgumentException"/>.
    /// </summary>
    [Fact]
    public void Constructor_WithDefaultProductId_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new PurchaseOrderItem(default, 3, _unitPrice));
    }

    /// <summary>
    /// Verifies that constructing a <see cref="PurchaseOrderItem"/> with a default <see cref="Money"/> throws an <see cref="ArgumentException"/>.
    /// </summary>
    [Fact]
    public void Constructor_WithDefaultUnitPrice_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new PurchaseOrderItem(_productId, 3, default));
    }

    /// <summary>
    /// Verifies that constructing a <see cref="PurchaseOrderItem"/> with zero or negative quantity throws an <see cref="ArgumentOutOfRangeException"/>.
    /// </summary>
    /// <param name="quantity">The non-positive quantity value.</param>
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void Constructor_WithZeroOrNegativeQuantity_ThrowsArgumentOutOfRangeException(int quantity)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new PurchaseOrderItem(_productId, quantity, _unitPrice));
    }

    /// <summary>
    /// Verifies that increasing the quantity with a positive amount increments the item quantity correctly.
    /// </summary>
    [Fact]
    public void IncreaseQuantity_WithPositiveAmount_IncreasesQuantity()
    {
        var item = new PurchaseOrderItem(_productId, 2, _unitPrice);

        item.IncreaseQuantity(5);

        Assert.Equal(7, item.Quantity);
    }

    /// <summary>
    /// Verifies that increasing the quantity with zero or negative amount throws an <see cref="ArgumentOutOfRangeException"/>.
    /// </summary>
    /// <param name="addition">The non-positive quantity increment.</param>
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void IncreaseQuantity_WithZeroOrNegativeAmount_ThrowsArgumentOutOfRangeException(int addition)
    {
        var item = new PurchaseOrderItem(_productId, 2, _unitPrice);

        Assert.Throws<ArgumentOutOfRangeException>(() => item.IncreaseQuantity(addition));
    }

    /// <summary>
    /// Verifies that <see cref="PurchaseOrderItem.CalculateItemTotal"/> returns the subtotal of unit price multiplied by quantity.
    /// </summary>
    [Fact]
    public void CalculateItemTotal_ReturnsQuantityMultipliedByUnitPrice()
    {
        var item = new PurchaseOrderItem(_productId, 4, new Money(12.50m, "USD"));

        var total = item.CalculateItemTotal();

        Assert.Equal(50.00m, total.Amount);
        Assert.Equal("USD", total.Currency.Code);
    }
}
