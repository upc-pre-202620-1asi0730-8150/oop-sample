using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

namespace ACME.OOP.Procurement.Domain.Model.Aggregates;

/// <summary>
/// Represents a purchase order aggregate root in the 'Procurement' bounded context. 
/// </summary>
public class PurchaseOrder
{
    private readonly List<PurchaseOrderItem> _items = [];

    public string OrderNumber { get; }
    public SupplierId SupplierId { get; }
    public DateOnly OrderDate { get; }
    public string Currency { get; }

    public IReadOnlyList<PurchaseOrderItem> Items => _items.AsReadOnly();

    /// <summary>
    /// Initializes a new instance of <see cref="PurchaseOrder"/>.
    /// </summary>
    /// <param name="orderNumber">The order number, which must be a non-null, non-empty string.</param>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="orderDate">The order date as a <see cref="DateOnly"/>.</param>
    /// <param name="currency">The currency 3-letter ISO code.</param>
    public PurchaseOrder(string orderNumber, SupplierId supplierId, DateOnly orderDate, string currency)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(orderNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(currency);
        if (currency.Length != 3)
            throw new ArgumentException("Currency must be a valid 3-letter ISO code.", nameof(currency));

        OrderNumber = orderNumber;
        SupplierId = supplierId;
        OrderDate = orderDate;
        Currency = currency.ToUpperInvariant();
    }

    /// <summary>
    /// Initializes a new instance of <see cref="PurchaseOrder"/> using a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="orderNumber">The order number.</param>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="orderDate">The order date.</param>
    /// <param name="currency">The currency 3-letter ISO code.</param>
    public PurchaseOrder(string orderNumber, SupplierId supplierId, DateTime orderDate, string currency)
        : this(orderNumber, supplierId, DateOnly.FromDateTime(orderDate), currency)
    {
    }

    /// <summary>
    /// Adds an item to the purchase order, merging quantities if the product already exists. 
    /// </summary>
    /// <param name="productId">The product identifier, which must be a non-null <see cref="ProductId"/> object.</param>
    /// <param name="quantity">The quantity of the product, which must be greater than zero.</param>
    /// <param name="unitPriceAmount">The unit price of the product, which must be a non-negative number.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the quantity is less than or equal to zero, or the unit price is negative.</exception>
    public void AddItem(ProductId productId, int quantity, decimal unitPriceAmount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegative(unitPriceAmount);

        var unitPrice = new Money(unitPriceAmount, Currency);
        var existingIndex = _items.FindIndex(item => item.ProductId == productId);

        if (existingIndex >= 0)
        {
            var existing = _items[existingIndex];
            _items[existingIndex] = new PurchaseOrderItem(productId, existing.Quantity + quantity, unitPrice);
        }
        else
        {
            _items.Add(new PurchaseOrderItem(productId, quantity, unitPrice));
        }
    }

    /// <summary>
    /// Calculates the total price of the purchase order. 
    /// </summary>
    /// <returns>The total price as a <see cref="Money"/> object.</returns>
    public Money CalculateTotal()
    {
        var total = new Money(0m, Currency);
        foreach (var item in _items) total += item.CalculateItemTotal();
        return total;
    }
}
