using ACME.OOP.Procurement.Domain.Model.Aggregates;
using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.SCM.Domain.Model.Aggregates;
using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

var supplierAddress = new Address("Supplier St", "123", "SupplierCity", null, "12345", "United States");
var supplier = new Supplier(new SupplierId("SUP001"), "Microsoft, Inc.", supplierAddress);

var purchaseOrder = new PurchaseOrder("PO001", supplier.Id, DateOnly.FromDateTime(DateTime.Now), "USD");
purchaseOrder.AddItem(ProductId.New(), 10, 25.99m);
purchaseOrder.AddItem(ProductId.New(), 20, 19.99m);

Console.WriteLine($"Purchase Order {purchaseOrder.OrderNumber} created for Supplier ID {purchaseOrder.SupplierId.Identifier} in {purchaseOrder.Currency}");
foreach (var item in purchaseOrder.Items)
{
    Console.WriteLine($"Order Item Total: {item.CalculateItemTotal()}");
}
Console.WriteLine($"Order Total: {purchaseOrder.CalculateTotal()}");
