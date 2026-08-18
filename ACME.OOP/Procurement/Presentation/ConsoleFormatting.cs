using ACME.OOP.Procurement.Domain.Model.Aggregates;

namespace ACME.OOP.Procurement.Presentation;

/// <summary>
/// Provides console formatting methods for purchase orders.
/// </summary>
internal static class ConsoleFormatting
{
    /// <summary>
    /// Formats a <see cref="PurchaseOrder"/> for display.
    /// </summary>
    /// <param name="order">The <see cref="PurchaseOrder"/> to format.</param>
    extension(PurchaseOrder order)
    {
        /// <summary>
        /// Returns a formatted string summary of the <see cref="PurchaseOrder"/>.
        /// </summary>
        public string Summary => $"Purchase Order {order.OrderNumber} created for Supplier ID {order.SupplierId.Identifier} in {order.Currency} on {order.OrderDate}";
    }
}