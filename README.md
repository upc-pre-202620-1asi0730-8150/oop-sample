# OOP Sample Project

This is a C# console application demonstrating object-oriented programming (OOP) and domain-driven design (DDD) principles within the context of Supply Chain Management (SCM) and Procurement domains.

**Author**: Web Applications Developer Team  
**License**: See [LICENSE.md](LICENSE.md) for details.

## Project Structure
- **Namespaces**:
    - `ACME.OOP.SCM` - Supply Chain Management bounded context
    - `ACME.OOP.Procurement` - Procurement bounded context
    - `ACME.OOP.Shared` - Shared value objects
- **Key Classes**:
    - `Supplier` and `SupplierId` (SCM)
    - `PurchaseOrder`, `PurchaseOrderItem`, and `ProductId` (Procurement)
    - `Money` and `Address` (Shared)

## Object-Oriented Design
The design follows DDD principles with aggregates and value objects. Key features:
- `Address` supports international formats with a required `Country` field and a distinct `Number` (street number) field.
- `PurchaseOrder` creates `PurchaseOrderItem` instances internally using a `ProductId` value object with a `Guid`.
- `ProductId` represents a product identifier as a globally unique `Guid`, as `Product` is out of scope.
- `PurchaseOrderItem` can calculate its own subtotal (quantity × unit price).
- `PurchaseOrder` enforces a single currency for all items as a business rule, accepting only the unit price amount and applying the order's currency.
- `PurchaseOrder` references a `Supplier` via a `SupplierId` value object from the SCM bounded context.

## Class Diagram
The following class diagram illustrates the structure of the application, including the main classes and their relationships.

![class-diagram](https://www.plantuml.com/plantuml/proxy?src=https://raw.githubusercontent.com/upc-pre-202520-1asi0730-sandbox/oop-sample/refs/heads/master/docs/class-diagram.puml?token=GHSAT0AAAAAADJGFNEY7HVSHSTV7PCH6XAK2FVAJNA)

See [docs/class-diagram.puml](docs/class-diagram.puml) for the PlantUML source.

## Usage
Run the console application to see a sample purchase order creation:
```bash
dotnet run
```