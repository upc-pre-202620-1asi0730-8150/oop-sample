# OOP Sample Project (`oop-sample`)

`oop-sample` is a reference .NET solution demonstrating **Object-Oriented Programming (OOP)**, **Domain-Driven Design (DDD)** tactical patterns, and modern **C# 14 / .NET 10** language and runtime features within Supply Chain Management and Procurement domains.

**Author**: Web Applications Developer Team  
**License**: See [LICENSE.md](LICENSE.md) for details.

---

## Technical Stack & Modern Features

* **Runtime & Framework**: .NET 10.0 (C# 14.0)
* **Testing Framework**: xUnit with `Microsoft.NET.Test.Sdk`
* **C# 14 & .NET 10 Features**:
  * **C# 14 Extension Members (`extension(T)`)**: Presentation formatting decoupled from core domain models using implicit extension declarations.
  * **`field` Keyword**: Property validation and fallback encapsulation without explicit private backing fields.
  * **Struct Parameterless Constructor Safety**: Explicit parameterless constructors throwing `InvalidOperationException` and null-safe property fallbacks on `readonly record struct` value objects.
  * **UUIDv7 Identifiers**: Time-ordered, sequential unique identifiers via `Guid.CreateVersion7()`.
  * **`DateOnly` Temporal Modeling**: Timezone- and time-of-day-free business dates for purchase orders.
  * **Collection Expressions (`[]`) & Modern Syntax**: Clean, expressive initialization across collections and arrays.
  * **Nullability & Modern Throw Helpers**: Strict nullable reference types (`<Nullable>enable</Nullable>`) and `ArgumentException.ThrowIfNullOrWhiteSpace`.

---

## Solution Structure

```text
oop-sample/
├── Acme.OOProgramming/             # Main domain & console application project
│   ├── Procurement/                # Procurement Bounded Context
│   │   ├── Domain/Model/
│   │   │   ├── Aggregates/         # PurchaseOrder (AR), PurchaseOrderItem (Entity)
│   │   │   └── ValueObjects/       # ProductId (UUIDv7)
│   │   └── Presentation/           # ConsoleFormatting (C# 14 extensions)
│   ├── SupplyChain/                # Supply Chain Bounded Context
│   │   ├── Domain/Model/
│   │   │   ├── Aggregates/         # Supplier (AR)
│   │   │   └── ValueObjects/       # SupplierId
│   ├── Shared/                     # Shared Kernel
│   │   ├── Domain/Model/
│   │   │   └── ValueObjects/       # Currency (ISO 4217), Money, Address
│   │   └── Presentation/           # ConsoleFormatting (C# 14 extensions)
│   └── Program.cs                  # Application entry point & demo scenarios
├── Acme.OOProgramming.Tests/       # Automated xUnit test suite (100 unit tests)
│   ├── Procurement/                # PurchaseOrder, PurchaseOrderItem, ProductId tests
│   ├── SupplyChain/                # Supplier, SupplierId tests
│   └── Shared/                     # Currency, Money, Address, ConsoleFormatting tests
├── docs/                           # Architecture & requirements documentation
│   ├── adrs.md                     # Architecture Decision Records (ADR 001–007)
│   ├── class-diagram.puml          # PlantUML domain model class diagram
│   └── user-stories.md             # User stories & Requirements Traceability Matrix (RTM)
├── CHANGELOG.md                    # Project release notes & version history
├── LICENSE.md                      # Project license
└── README.md                       # Project overview & guide
```

---

## Bounded Contexts & Domain Model

The domain architecture is divided into cohesive Bounded Contexts and a Shared Kernel:

### 1. `Acme.OOProgramming.SupplyChain` (Supply Chain)
* **`Supplier`** (*Aggregate Root*): Represents a vendor entity with a unique identity (`SupplierId`), legal name, and internationalized `Address`.
* **`SupplierId`** (*Value Object*): Strongly-typed identifier ensuring supplier references remain valid and explicit.

### 2. `Acme.OOProgramming.Procurement` (Procurement)
* **`PurchaseOrder`** (*Aggregate Root*): Encapsulates purchase order invariants, order dates, single-currency consistency, and line item lifecycle.
* **`PurchaseOrderItem`** (*Entity*): Internal aggregate entity managing quantities, unit prices, and subtotal calculations.
* **`ProductId`** (*Value Object*): Time-ordered sequential identifier generated using UUIDv7 (`Guid.CreateVersion7()`).

### 3. `Acme.OOProgramming.Shared` (Shared Kernel)
* **`Currency`** (*Value Object*): Represents ISO 4217 3-letter alphabetic currency codes (e.g., `USD`, `EUR`).
* **`Money`** (*Value Object*): Immutable monetary value object with currency validation and native operator overloads (`+`, `*`).
* **`Address`** (*Value Object*): Internationalized postal address with validation for street, number, city, postal code, and country.

---

## Key Domain Rules & Design Invariants

* **Aggregate Invariant Encapsulation**: `PurchaseOrder` strictly manages the lifecycle of `PurchaseOrderItem`. Direct instantiation or external mutation of items is prohibited.
* **Single-Currency Consistency**: `PurchaseOrder` enforces a single currency across all line items and order totals.
* **Duplicate Line Item Merging & Conflict Handling**: `PurchaseOrder.AddItem` automatically merges quantities when an existing `ProductId` is added at the same unit price; attempting to add an existing product at a conflicting unit price is rejected with an `InvalidOperationException`.
* **Currency-Safe Arithmetic**: `Money` prevents cross-currency operations at runtime and supports intuitive arithmetic operator overloads (`+`, `*`).
* **Struct Default Safety**: Value objects modeled as `readonly record struct` guard against uninitialized `new Struct()` calls with explicit parameterless constructors and null-safe property fallbacks.
* **Defensive Copying & Allocation Optimization**: `PurchaseOrder.Items` provides an immutable view via a cached read-only collection wrapper (`_itemsView`), avoiding repetitive allocations.
* **Cross-Context Reference by ID**: Bounded contexts reference foreign aggregates via strongly-typed identifiers (`SupplierId`) rather than direct object references.

---

## Project Documentation

| Document | Description |
| :--- | :--- |
| [**Architecture Decision Records (ADRs)**](docs/adrs.md) | Comprehensive architectural decisions (ADR 001 through ADR 007 in MADR format). |
| [**User Stories & RTM**](docs/user-stories.md) | Business requirements, user stories (US001–US004), and Requirements Traceability Matrix. |
| [**Class Diagram**](docs/class-diagram.puml) | PlantUML class diagram illustrating bounded contexts, aggregates, entities, and value objects. |
| [**Changelog**](CHANGELOG.md) | Version history, recent architectural improvements, and release notes. |
| [**License**](LICENSE.md) | Project licensing information. |

---

## Getting Started

### Prerequisites
* [.NET 10 SDK](https://dotnet.microsoft.com/download) (or later)

### Build the Solution
```bash
dotnet build
```

### Run the Application
```bash
dotnet run --project Acme.OOProgramming
```

### Run the Automated Test Suite
```bash
dotnet test
```
