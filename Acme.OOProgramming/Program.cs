using Acme.OOProgramming.Procurement.Domain.Model.Aggregates;
using Acme.OOProgramming.Procurement.Domain.Model.ValueObjects;
using Acme.OOProgramming.Procurement.Presentation;
using Acme.OOProgramming.Shared.Domain.Model.ValueObjects;
using Acme.OOProgramming.Shared.Presentation;
using Acme.OOProgramming.SupplyChain.Domain.Model.Aggregates;
using Acme.OOProgramming.SupplyChain.Domain.Model.ValueObjects;

var supplierAddress = new Address("Supplier St", "123", "SupplierCity", null, "12345", "United States");
var supplier = new Supplier(new SupplierId("SUP001"), "Microsoft, Inc.", supplierAddress);
var salesOfDay = new Money(0, "USD");
var purchaseOrder = new PurchaseOrder("PO001", supplier.Id, DateOnly.FromDateTime(DateTime.Now), "USD");
var sharedProduct = ProductId.New();
purchaseOrder.AddItem(sharedProduct, 10, 25.99m);
purchaseOrder.AddItem(sharedProduct, 5, 25.99m);
purchaseOrder.AddItem(ProductId.New(), 20, 19.99m);

Console.WriteLine(purchaseOrder.Summary);
foreach (var item in purchaseOrder.Items)
{
    Console.Write($"Order Item: {item.ProductId} x {item.Quantity} at Unit Price of {item.UnitPrice} ");
    Console.WriteLine($"Results in Order Item Total: {item.CalculateItemTotal()}");
}

Console.WriteLine($"Order Total: {purchaseOrder.CalculateTotal()}");

try
{
    purchaseOrder.AddItem(sharedProduct, 1, 9.99m);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Rejected conflicting unit price: {ex.Message}");
}

Console.WriteLine($"Sales for the day: {salesOfDay.Add(purchaseOrder.CalculateTotal()).Display}");

Console.WriteLine($"Supplier: {supplier.Name} is located at {supplier.Address}");