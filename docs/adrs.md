# Architecture Decision Records (ADRs)

**Author**: Web Applications Developer Team  
**License**: See [LICENSE.md](../LICENSE.md) for details.

This document records the architectural and design decisions made for the ACME OOP Sample project.

---

## Index of ADRs

* [ADR 001: Bounded Context Architecture (SupplyChain, Procurement, Shared Kernel)](#adr-001-bounded-context-architecture-supplychain-procurement-shared-kernel)
* [ADR 002: Aggregate Roots and Invariant Encapsulation for Purchase Orders](#adr-002-aggregate-roots-and-invariant-encapsulation-for-purchase-orders)
* [ADR 003: Immutability and Value Semantics via `readonly record struct`](#adr-003-immutability-and-value-semantics-via-readonly-record-struct)
* [ADR 004: Time-Ordered UUIDv7 for Product Identifiers](#adr-004-time-ordered-uuidv7-for-product-identifiers)
* [ADR 005: Temporal Modeling with `DateOnly` for Order Dates](#adr-005-temporal-modeling-with-dateonly-for-order-dates)
* [ADR 006: Presentation Decoupling via C# 14 Extension Members](#adr-006-presentation-decoupling-via-c-14-extension-members)
* [ADR 007: Property Invariant Validation using C# 14 `field` Keyword](#adr-007-property-invariant-validation-using-c-14-field-keyword)

---

## ADR 001: Bounded Context Architecture (SupplyChain, Procurement, Shared Kernel)

### Status
Accepted

### Context
The application needs to model supply chain management and purchasing workflows. Combining these concepts into a single unstructured domain model would lead to tight coupling, conflicting terminology, and bloated domain models.

### Decision
We partition the domain into distinct Bounded Contexts following Domain-Driven Design (DDD):
1. **Supply Chain Management (`Acme.OOProgramming.SupplyChain`)**: Focuses on suppliers, supplier identities, and vendor profiles.
2. **Procurement (`Acme.OOProgramming.Procurement`)**: Focuses on purchase orders, line items, and purchasing workflows.
3. **Shared Kernel (`Acme.OOProgramming.Shared`)**: Contains common value objects shared across contexts (`Money`, `Currency`, `Address`).

Cross-context references (e.g., `PurchaseOrder` referencing a supplier) use strongly typed identifiers (`SupplierId`) rather than direct aggregate references.

### Consequences
* **Positive**: High cohesion within contexts, clear boundaries, and decoupled domain evolution.
* **Negative**: Requires explicit mapping and referencing by ID when navigating across context boundaries.

---

## ADR 002: Aggregate Roots and Invariant Encapsulation for Purchase Orders

### Status
Accepted

### Context
Purchase orders contain line items that must follow specific business rules:
* All line items must share the order's currency.
* Total and item subtotal calculations must be consistent and guarded against negative quantities or prices.
* Adding items with an existing product ID should merge quantities rather than create conflicting lines, provided the unit price matches.
* The lifecycle of line items is strictly bound to the purchase order.

### Decision
We model `PurchaseOrder` as an **Aggregate Root** and `PurchaseOrderItem` as an internal entity within the aggregate boundary:
* External callers cannot instantiate or modify `PurchaseOrderItem` directly; all item additions are mediated by `PurchaseOrder.AddItem(...)`.
* `PurchaseOrder` creates line items using its own currency, ensuring single-currency consistency across all items.
* `AddItem` handles quantity merging for duplicate products and rejects conflicting unit prices.
* The items collection is exposed to callers as a read-only list (`IReadOnlyList<PurchaseOrderItem>`) backed by a cached read-only view (`_itemsView`) to eliminate per-access allocation overhead.

### Consequences
* **Positive**: Domain invariants are strictly protected; invalid aggregate states are impossible.
* **Positive**: Consistent calculations for line totals and order totals.
* **Positive**: Efficient zero-allocation access to aggregate item collections.
* **Negative**: Line items cannot be manipulated independently of the aggregate root.

---

## ADR 003: Immutability and Value Semantics via `readonly record struct`

### Status
Accepted

### Context
Domain concepts such as `Money`, `Currency`, `Address`, `ProductId`, and `SupplierId` represent values without distinct mutable lifecycle identities. Using reference types (`class`) creates unnecessary heap allocations, garbage collection overhead, and mutable state risks. Furthermore, monetary arithmetic must prevent adding mismatched currencies and protect against uninitialized struct defaults.

### Decision
We implement all domain Value Objects as C# `readonly record struct`:
* **Zero Allocation**: Instances are allocated on the stack or inline within containing aggregates.
* **Structural Equality**: Equality is based on value fields rather than reference identity.
* **Invariant Enforcement**: Constructors validate arguments using standard .NET throw helpers (`ArgumentException.ThrowIfNullOrWhiteSpace`, `ArgumentOutOfRangeException.ThrowIfNegative`).
* **Default State Safety**: Value types define explicit parameterless constructors throwing `InvalidOperationException` and null-safe property accessors (`field ?? string.Empty`) to prevent uninitialized `default` struct states from corrupting domain models.
* **Operator Overloads**: `Money` defines explicit operators (`+`, `*`) enforcing currency parity and initialized state checks before performing arithmetic.

### Consequences
* **Positive**: High performance and zero GC overhead for value objects.
* **Positive**: Enforces compile-time and runtime immutability.
* **Positive**: Prevents uninitialized default struct states and cross-currency calculation bugs.

---

## ADR 004: Time-Ordered UUIDv7 for Product Identifiers

### Status
Accepted

### Context
`ProductId` must provide unique identification across distributed systems without relying on centralized database sequence generators. Standard random UUIDs (UUIDv4 via `Guid.NewGuid()`) suffer from poor database index locality and lack temporal sortability.

### Decision
We adopt **UUIDv7** via .NET's `Guid.CreateVersion7()` for `ProductId.New()`.

### Consequences
* **Positive**: Generated identifiers are monotonically increasing over time, optimizing B-tree index performance in storage engines.
* **Positive**: Preserves global uniqueness without requiring a database round-trip.
* **Negative**: IDs carry an embedded timestamp, which may reveal entity creation time.

---

## ADR 005: Temporal Modeling with `DateOnly` for Order Dates

### Status
Accepted

### Context
A purchase order date represents a calendar business date rather than an instantaneous timestamp with milliseconds and timezone offsets. Using `System.DateTime` introduces timezone ambiguity, Daylight Saving Time complications, and meaningless time components (e.g., `12:00:00 AM`).

### Decision
We use `System.DateOnly` for `PurchaseOrder.OrderDate`.

### Consequences
* **Positive**: Accurately reflects domain intent (calendar date of purchase).
* **Positive**: Eliminates timezone conversion and time-of-day bugs.
* **Negative**: Callers passing `DateTime` must convert using `DateOnly.FromDateTime(...)` (convenience constructor provided).

---

## ADR 006: Presentation Decoupling via C# 14 Extension Members

### Status
Accepted

### Context
Domain entities and value objects (such as `PurchaseOrder` and `Money`) should remain pure and decoupled from display, UI, or console formatting concerns. Adding string formatting methods directly to domain models mixes domain logic with presentation concerns.

### Decision
We use C# 14 implicit extension declarations (`extension(T target)`) located in separate presentation namespaces (`Acme.OOProgramming.Procurement.Presentation`, `Acme.OOProgramming.Shared.Presentation`) to provide presentation-specific properties and methods (e.g., `order.Summary`, `money.Display`) without polluting the domain models.

### Consequences
* **Positive**: Pure domain models adhering to Single Responsibility Principle (SRP).
* **Positive**: Clean, discoverable syntax at call sites without requiring domain class modifications.
* **Negative**: Presentation extension namespaces must be imported where display properties are consumed.

---

## ADR 007: Property Invariant Validation using C# 14 `field` Keyword

### Status
Accepted

### Context
Value objects and domain entities require validation in property setters or init-only accessors. Historically in C#, validating property values required declaring explicit, boilerplate private backing fields (e.g., `_code`, `_street`).

### Decision
We adopt the C# 14 `field` contextual keyword in property `init` and `get` accessors across domain models and value objects to enforce validations and fallback values directly without manual backing field declarations.

### Consequences
* **Positive**: Significantly reduces repetitive boilerplate code.
* **Positive**: Keeps property definition, validation logic, and storage backing cohesive and readable.
* **Negative**: Requires C# 14 language version compiler support.
