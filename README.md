# OOP Sample Project (oop-sample)

OOP-Sample is a C# console application demonstrating **Object-Oriented Programming (OOP)**, **Domain-Driven Design (DDD)** tactical patterns, and **C# 14 / .NET 10** features within the context of Supply Chain and Procurement domains.

**Author**: Web Applications Developer Team  
**License**: See [LICENSE.md](LICENSE.md) for details.

---

## Technical Stack & Prerequisites

* **Runtime & SDK**: .NET 10.0+ / C# 14
* **Language Features**: `readonly record struct`, UUIDv7 (`Guid.CreateVersion7()`), collection expressions (`[]`), modern throw helpers, and `DateOnly` modeling.

---

## Bounded Contexts & Domain Model

The solution is divided into cohesive Bounded Contexts and a Shared Kernel:

### 1. `ACME.OOP.SupplyChain` (Supply Chain)
* **`Supplier`** (*Aggregate Root*): Represents a vendor with identity and location.
* **`SupplierId`** (*Value Object*): Strongly-typed identifier for supplier references.

### 2. `ACME.OOP.Procurement` (Procurement)
* **`PurchaseOrder`** (*Aggregate Root*): Encapsulates purchase order invariants, currency consistency, and line item lifecycle.
* **`PurchaseOrderItem`** (*Entity*): Internal aggregate entity calculating line item totals.
* **`ProductId`** (*Value Object*): Time-ordered sequential identifier generated using UUIDv7 (`Guid.CreateVersion7()`).

### 3. `ACME.OOP.Shared` (Shared Kernel)
* **`Currency`** (*Value Object*): Represents ISO 4217 3-letter alphabetic currency codes.
* **`Money`** (*Value Object*): Immutable monetary value object with currency validation and operator overloads (`+`, `*`).
* **`Address`** (*Value Object*): Internationalized postal address.

---

## Key Domain Rules & Design Principles

* **Aggregate Invariant Encapsulation**: `PurchaseOrder` strictly controls the creation and lifecycle of `PurchaseOrderItem`. Direct instantiation outside the aggregate is prohibited.
* **Single-Currency Rule**: `PurchaseOrder` enforces a single currency across all line items and safeguards order calculations.
* **Currency-Safe Arithmetic**: `Money` prevents cross-currency operations at runtime and supports native operator overloads (`+`, `*`).
* **Duplicate Line Item Handling**: `PurchaseOrder.AddItem` merges quantities when an existing `ProductId` is added.
* **Zero-Allocation Value Objects**: Small value objects (`Money`, `Currency`, `Address`, `ProductId`, `SupplierId`) are modeled as `readonly record struct` for stack allocation and structural equality.
* **Temporal Accuracy**: `PurchaseOrder.OrderDate` uses `DateOnly` to eliminate timezone and time-of-day ambiguity for business orders.
* **Cross-Context References**: Bounded contexts reference foreign aggregates via strongly-typed IDs (`SupplierId`) rather than direct object references.

---

## Project Documentation

| Document | Description |
| :--- | :--- |
| [**Architecture Decision Records (ADRs)**](docs/adrs.md) | Architectural and design decisions (MADR format). |
| [**User Stories**](docs/user-stories.md) | Business requirements and domain user stories. |
| [**Class Diagram**](docs/class-diagram.puml) | PlantUML domain model class diagram. |
| [**Changelog**](CHANGELOG.md) | Version history and notable changes. |
| [**License**](LICENSE.md) | Project licensing details. |

---

## Getting Started

### Build the Solution
```bash
dotnet build
```

### Run the Application
```bash
dotnet run --project ACME.OOP
```
