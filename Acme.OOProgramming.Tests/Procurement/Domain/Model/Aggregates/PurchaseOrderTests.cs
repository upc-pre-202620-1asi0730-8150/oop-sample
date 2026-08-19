using Acme.OOProgramming.Procurement.Domain.Model.Aggregates;
using Acme.OOProgramming.Procurement.Domain.Model.ValueObjects;
using Acme.OOProgramming.Shared.Domain.Model.ValueObjects;
using Acme.OOProgramming.SupplyChain.Domain.Model.ValueObjects;

namespace Acme.OOProgramming.Tests.Procurement.Domain.Model.Aggregates;

public class PurchaseOrderTests
{
    private readonly SupplierId _supplierId = new("SUP-001");
    private readonly Currency _usd = new("USD");
    private readonly DateOnly _orderDate = new(2026, 8, 18);

    [Fact]
    public void Constructor_WithValidArguments_InitializesSuccessfully()
    {
        var order = new PurchaseOrder("PO-1001", _supplierId, _orderDate, _usd);

        Assert.Equal("PO-1001", order.OrderNumber);
        Assert.Equal(_supplierId, order.SupplierId);
        Assert.Equal(_orderDate, order.OrderDate);
        Assert.Equal(_usd, order.Currency);
        Assert.Empty(order.Items);
    }

    [Fact]
    public void Constructor_WithStringCurrency_InitializesSuccessfully()
    {
        var order = new PurchaseOrder("PO-1002", _supplierId, _orderDate, "EUR");

        Assert.Equal("PO-1002", order.OrderNumber);
        Assert.Equal("EUR", order.Currency.Code);
    }

    [Fact]
    public void Constructor_WithDateTime_InitializesSuccessfully()
    {
        var dateTime = new DateTime(2026, 8, 18, 14, 30, 0);
        var order = new PurchaseOrder("PO-1003", _supplierId, dateTime, "USD");

        Assert.Equal(new DateOnly(2026, 8, 18), order.OrderDate);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespaceOrderNumber_ThrowsArgumentException(string? orderNumber)
    {
        Assert.ThrowsAny<ArgumentException>(() => new PurchaseOrder(orderNumber!, _supplierId, _orderDate, _usd));
    }

    [Fact]
    public void Constructor_WithDefaultSupplierId_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new PurchaseOrder("PO-1001", default(SupplierId), _orderDate, _usd));
    }

    [Fact]
    public void Constructor_WithDefaultCurrency_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new PurchaseOrder("PO-1001", _supplierId, _orderDate, default(Currency)));
    }

    [Fact]
    public void Items_ReturnsCachedReadOnlyView()
    {
        var order = new PurchaseOrder("PO-1001", _supplierId, _orderDate, _usd);

        var view1 = order.Items;
        var view2 = order.Items;

        Assert.Same(view1, view2);
    }

    [Fact]
    public void AddItem_WithValidArguments_AddsItemToOrder()
    {
        var order = new PurchaseOrder("PO-1001", _supplierId, _orderDate, _usd);
        var productId = ProductId.New();

        order.AddItem(productId, 5, 19.99m);

        Assert.Single(order.Items);
        var item = order.Items[0];
        Assert.Equal(productId, item.ProductId);
        Assert.Equal(5, item.Quantity);
        Assert.Equal(19.99m, item.UnitPrice.Amount);
        Assert.Equal(_usd, item.UnitPrice.Currency);
    }

    [Fact]
    public void AddItem_WithDefaultProductId_ThrowsArgumentException()
    {
        var order = new PurchaseOrder("PO-1001", _supplierId, _orderDate, _usd);

        Assert.Throws<ArgumentException>(() => order.AddItem(default, 5, 19.99m));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void AddItem_WithZeroOrNegativeQuantity_ThrowsArgumentOutOfRangeException(int quantity)
    {
        var order = new PurchaseOrder("PO-1001", _supplierId, _orderDate, _usd);
        var productId = ProductId.New();

        Assert.Throws<ArgumentOutOfRangeException>(() => order.AddItem(productId, quantity, 19.99m));
    }

    [Fact]
    public void AddItem_WithNegativeUnitPrice_ThrowsArgumentOutOfRangeException()
    {
        var order = new PurchaseOrder("PO-1001", _supplierId, _orderDate, _usd);
        var productId = ProductId.New();

        Assert.Throws<ArgumentOutOfRangeException>(() => order.AddItem(productId, 5, -10.00m));
    }

    [Fact]
    public void AddItem_WithDuplicateProductAndMatchingPrice_MergesQuantity()
    {
        var order = new PurchaseOrder("PO-1001", _supplierId, _orderDate, _usd);
        var productId = ProductId.New();

        order.AddItem(productId, 10, 25.00m);
        order.AddItem(productId, 5, 25.00m);

        Assert.Single(order.Items);
        Assert.Equal(15, order.Items[0].Quantity);
        Assert.Equal(25.00m, order.Items[0].UnitPrice.Amount);
    }

    [Fact]
    public void AddItem_WithDuplicateProductAndConflictingPrice_ThrowsInvalidOperationException()
    {
        var order = new PurchaseOrder("PO-1001", _supplierId, _orderDate, _usd);
        var productId = ProductId.New();

        order.AddItem(productId, 10, 25.00m);

        Assert.Throws<InvalidOperationException>(() => order.AddItem(productId, 5, 30.00m));
    }

    [Fact]
    public void CalculateTotal_WithEmptyOrder_ReturnsZeroMoneyInOrderCurrency()
    {
        var order = new PurchaseOrder("PO-1001", _supplierId, _orderDate, _usd);

        var total = order.CalculateTotal();

        Assert.Equal(0m, total.Amount);
        Assert.Equal(_usd, total.Currency);
    }

    [Fact]
    public void CalculateTotal_WithMultipleItems_CalculatesAccurateTotal()
    {
        var order = new PurchaseOrder("PO-1001", _supplierId, _orderDate, _usd);
        var p1 = ProductId.New();
        var p2 = ProductId.New();

        order.AddItem(p1, 2, 10.50m); // 21.00
        order.AddItem(p2, 3, 5.00m);  // 15.00

        var total = order.CalculateTotal();

        Assert.Equal(36.00m, total.Amount);
        Assert.Equal(_usd, total.Currency);
    }
}
