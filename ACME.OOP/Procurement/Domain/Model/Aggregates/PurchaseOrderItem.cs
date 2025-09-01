using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

namespace ACME.OOP.Procurement.Domain.Model.Aggregates;

/// <summary>
/// Represents a purchase order item aggregate in the Procurement bounded context. 
/// </summary>
/// <param name="productId">The product identifier, which must be a non-null <see cref="ProductId"/> object.</param>
/// <param name="quantity">The quantity of the product, which must be greater than zero.</param>
/// <param name="unitPrice">The unit price of the product, which is a non-null <see cref="Money"/> object.</param>
public class PurchaseOrderItem(ProductId productId, int quantity, Money unitPrice)
{
    public ProductId ProductId { get; } = productId ?? throw new ArgumentNullException(nameof(productId), "Product ID cannot be null.");
    public int Quantity { get; } = quantity > 0 ? quantity : throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
    public Money UnitPrice { get; } = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice), "Unit price cannot be null.");
    
    /// <summary>
    /// Calculates the total price of the item. 
    /// </summary>
    /// <returns>The total price as a <see cref="Money"/> object.</returns>
    public Money CalculateItemTotal() => new(UnitPrice.Amount * Quantity, UnitPrice.Currency);
}