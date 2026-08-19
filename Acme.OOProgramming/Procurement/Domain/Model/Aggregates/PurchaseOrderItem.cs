using Acme.OOProgramming.Procurement.Domain.Model.ValueObjects;
using Acme.OOProgramming.Shared.Domain.Model.ValueObjects;

namespace Acme.OOProgramming.Procurement.Domain.Model.Aggregates;

/// <summary>
/// Represents a purchase order item entity in the Procurement bounded context. 
/// </summary>
public class PurchaseOrderItem
{
    /// <summary>
    /// The product identifier.
    /// </summary>
    public ProductId ProductId { get; }

    /// <summary>
    /// The quantity of the product.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the quantity is less than or equal to zero.</exception>
    public int Quantity
    {
        get;
        private set
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
            field = value;
        }
    }

    /// <summary>
    /// The unit price of the product.
    /// </summary>
    public Money UnitPrice { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="PurchaseOrderItem"/>. 
    /// </summary>
    /// <param name="productId">The product identifier, which must be a non-null <see cref="ProductId"/> object.</param>
    /// <param name="quantity">The quantity of the product, which must be greater than zero.</param>
    /// <param name="unitPrice">The unit price of the product, which is a non-null <see cref="Money"/> object.</param>
    internal PurchaseOrderItem(ProductId productId, int quantity, Money unitPrice)
    {
        if (productId == default)
            throw new ArgumentException("Product ID is required.", nameof(productId));
        if (unitPrice == default)
            throw new ArgumentException("Unit price is required.", nameof(unitPrice));
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    /// <summary>
    /// Increases the quantity of the item by the specified amount.
    /// </summary>
    /// <param name="additionalQuantity">The additional quantity to add, which must be greater than zero.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the additional quantity is less than or equal to zero.</exception>
    internal void IncreaseQuantity(int additionalQuantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(additionalQuantity);
        Quantity += additionalQuantity;
    }
    
    /// <summary>
    /// Calculates the total price of the item. 
    /// </summary>
    /// <returns>The total price as a <see cref="Money"/> object.</returns>
    public Money CalculateItemTotal() => UnitPrice * Quantity;
}
