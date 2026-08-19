using Acme.OOProgramming.Procurement.Domain.Model.Aggregates;
using Acme.OOProgramming.Procurement.Presentation;
using Acme.OOProgramming.Shared.Domain.Model.ValueObjects;
using Acme.OOProgramming.SupplyChain.Domain.Model.ValueObjects;

namespace Acme.OOProgramming.Tests.Procurement.Presentation;

public class ConsoleFormattingTests
{
    [Fact]
    public void Summary_ReturnsFormattedPurchaseOrderSummary()
    {
        var supplierId = new SupplierId("SUP-001");
        var currency = new Currency("USD");
        var orderDate = new DateOnly(2026, 8, 18);
        var order = new PurchaseOrder("PO-1001", supplierId, orderDate, currency);

        var summary = order.Summary;

        Assert.Equal($"Purchase Order PO-1001 created for Supplier ID SUP-001 in USD on {orderDate}", summary);
    }
}
