using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

namespace ACME.OOP.Procurement.Domain.Model.Aggregates;

/// <summary>
/// Represents a purchase order aggregate in the Procurement bounded context. 
/// </summary>
/// <param name="orderNumber">The order number, which must be a non-null, non-empty string.</param>
/// <param name="supplierId">The supplier identifier, which must be a non-null <see cref="SupplierId"/> object.</param>
/// <param name="orderDate">The order date, which must be a non-null <see cref="DateTime"/> object.</param>
/// <param name="currency">The currency, which must be a non-null, non-empty string with a length of 3.</param>
public class PurchaseOrder(string orderNumber, SupplierId supplierId, DateTime orderDate, string currency)
{
    private readonly List<PurchaseOrderItem> _items = new();

    public string OrderNumber { get; } = orderNumber ?? throw new ArgumentNullException(nameof(orderNumber));
    public SupplierId SupplierId { get; } = supplierId ?? throw new ArgumentNullException(nameof(supplierId));
    public DateTime OrderDate { get; } = orderDate;
    public string Currency { get; } = string.IsNullOrWhiteSpace(currency) || currency.Length != 3
        ? throw new ArgumentException("Currency must be a valid 3-letter code.", nameof(currency))
        : currency;

    public IReadOnlyList<PurchaseOrderItem> Items => _items.AsReadOnly();

    /// <summary>
    /// Adds an item to the purchase order. 
    /// </summary>
    /// <param name="productId">The product identifier, which must be a non-null <see cref="ProductId"/> object.</param>
    /// <param name="quantity">The quantity of the product, which must be greater than zero.</param>
    /// <param name="unitPriceAmount">The unit price of the product, which must be a non-negative number.</param>
    /// <exception cref="ArgumentNullException">Thrown when any required parameter is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the quantity is less than or equal to zero, or the unit price is negative.</exception>
    public void AddItem(ProductId productId, int quantity, decimal unitPriceAmount)
    {
        ArgumentNullException.ThrowIfNull(productId);
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        if (unitPriceAmount < 0) throw new ArgumentOutOfRangeException(nameof(unitPriceAmount), "Unit price amount cannot be negative.");

        var unitPrice = new Money(unitPriceAmount, Currency);
        var item = new PurchaseOrderItem(productId, quantity, unitPrice);
        _items.Add(item);
    }

    /// <summary>
    /// Calculates the total price of the purchase order. 
    /// </summary>
    /// <returns>The total price as a <see cref="Money"/> object.</returns>
    public Money CalculateTotal()
    {
        var total = _items.Sum(item => item.CalculateItemTotal().Amount);
        return new Money(total, Currency);
    }
}