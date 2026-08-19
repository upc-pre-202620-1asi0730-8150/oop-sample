using Acme.OOProgramming.Procurement.Domain.Model.ValueObjects;
using Acme.OOProgramming.Shared.Domain.Model.ValueObjects;
using Acme.OOProgramming.SupplyChain.Domain.Model.ValueObjects;

namespace Acme.OOProgramming.Procurement.Domain.Model.Aggregates;

/// <summary>
/// Represents a purchase order aggregate root in the Procurement bounded context. 
/// </summary>
public class PurchaseOrder
{
    private readonly List<PurchaseOrderItem> _items = [];
    private IReadOnlyList<PurchaseOrderItem>? _itemsView;

    /// <summary>
    /// Gets the unique purchase order number.
    /// </summary>
    public string OrderNumber { get; }

    /// <summary>
    /// Gets the identifier of the supplier associated with this purchase order.
    /// </summary>
    public SupplierId SupplierId { get; }

    /// <summary>
    /// Gets the order date.
    /// </summary>
    public DateOnly OrderDate { get; }

    /// <summary>
    /// Gets the currency enforced across all items in this purchase order.
    /// </summary>
    public Currency Currency { get; }

    /// <summary>
    /// Gets an immutable read-only view of the purchase order items.
    /// </summary>
    public IReadOnlyList<PurchaseOrderItem> Items => _itemsView ??= _items.AsReadOnly();

    /// <summary>
    /// Initializes a new instance of <see cref="PurchaseOrder"/>.
    /// </summary>
    /// <param name="orderNumber">The order number, which must be a non-null, non-empty string.</param>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="orderDate">The order date as a <see cref="DateOnly"/>.</param>
    /// <param name="currency">The currency 3-letter ISO code.</param>
    public PurchaseOrder(string orderNumber, SupplierId supplierId, DateOnly orderDate, Currency currency)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(orderNumber);
        if (supplierId == default)
            throw new ArgumentException("Supplier ID is required.", nameof(supplierId));
        if (currency == default)
            throw new ArgumentException("Currency is required.", nameof(currency));

        OrderNumber = orderNumber;
        SupplierId = supplierId;
        OrderDate = orderDate;
        Currency = currency;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="PurchaseOrder"/> using a <see cref="DateOnly"/>.
    /// </summary>
    /// <param name="orderNumber">The order number.</param>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="orderDate">The order date.</param>
    /// <param name="currency">The currency 3-letter ISO code.</param>
    /// <exception cref="ArgumentException">Thrown when the currency is not a valid 3-letter ISO code.</exception>
    public PurchaseOrder(string orderNumber, SupplierId supplierId, DateOnly orderDate, string currency)
        : this(orderNumber, supplierId, orderDate, new Currency(currency)) { }
    
    /// <summary>
    /// Initializes a new instance of <see cref="PurchaseOrder"/> using a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="orderNumber">The order number.</param>
    /// <param name="supplierId">The supplier identifier.</param>
    /// <param name="orderDate">The order date.</param>
    /// <param name="currency">The currency 3-letter ISO code.</param>
    public PurchaseOrder(string orderNumber, SupplierId supplierId, DateTime orderDate, string currency)
        : this(orderNumber, supplierId, DateOnly.FromDateTime(orderDate), new Currency(currency)) { }

    
    /// <summary>
    /// Adds an item to the purchase order, merging quantities if the product already exists. 
    /// </summary>
    /// <param name="productId">The product identifier, which must be a non-null <see cref="ProductId"/> object.</param>
    /// <param name="quantity">The quantity of the product, which must be greater than zero.</param>
    /// <param name="unitPriceAmount">The unit price of the product, which must be a non-negative number.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the quantity is less than or equal to zero, or the unit price is negative.</exception>
    /// <exception cref="ArgumentException">Thrown when the product ID is null or empty.</exception>
    public void AddItem(ProductId productId, int quantity, decimal unitPriceAmount)
    {
        if (productId == default)
            throw new ArgumentException("Product ID is required.", nameof(productId));
        
        var unitPrice = new Money(unitPriceAmount, Currency);
        var existing = _items.Find(item => item.ProductId == productId);

        if (existing is not null)
        {
            if (existing.UnitPrice != unitPrice)
                throw new InvalidOperationException($"Cannot add product {productId} at {unitPrice}; the order already has it at {existing.UnitPrice}.");
            existing.IncreaseQuantity(quantity);
            return;
        }
        _items.Add(new PurchaseOrderItem(productId, quantity, unitPrice));
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
