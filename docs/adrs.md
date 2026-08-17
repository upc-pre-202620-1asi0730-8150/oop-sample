# Architecture Decision Records (ADRs)

**Author**: Web Applications Developer Team  
**License**: See [LICENSE.md](../LICENSE.md) for details.

This document records the architectural and design decisions made for the ACME OOP Sample project.

---

## Index of ADRs

* [ADR 001: Bounded Context Architecture (SCM, Procurement, Shared Kernel)](#adr-001-bounded-context-architecture-scm-procurement-shared-kernel)
* [ADR 002: Aggregate Roots and Invariant Encapsulation for Purchase Orders](#adr-002-aggregate-roots-and-invariant-encapsulation-for-purchase-orders)
* [ADR 003: Immutability and Value Semantics via `readonly record struct`](#adr-003-immutability-and-value-semantics-via-readonly-record-struct)
* [ADR 004: Time-Ordered UUIDv7 for Product Identifiers](#adr-004-time-ordered-uuidv7-for-product-identifiers)
* [ADR 005: Temporal Modeling with `DateOnly` for Order Dates](#adr-005-temporal-modeling-with-dateonly-for-order-dates)

---

## ADR 001: Bounded Context Architecture (SCM, Procurement, Shared Kernel)

### Status
Accepted

### Context
The application needs to model supply chain management and purchasing workflows. Combining these concepts into a single unstructured domain model would lead to tight coupling, conflicting terminology, and bloated domain models.

### Decision
We partition the domain into distinct Bounded Contexts following Domain-Driven Design (DDD):
1. **Supply Chain Management (`ACME.OOP.SCM`)**: Focuses on suppliers, supplier identities, and vendor profiles.
2. **Procurement (`ACME.OOP.Procurement`)**: Focuses on purchase orders, line items, and purchasing workflows.
3. **Shared Kernel (`ACME.OOP.Shared`)**: Contains common value objects shared across contexts (`Money`, `Address`).

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
* Adding items with an existing product ID should merge quantities rather than create conflicting lines.
* The lifecycle of line items is strictly bound to the purchase order.

### Decision
We model `PurchaseOrder` as an **Aggregate Root** and `PurchaseOrderItem` as an internal entity within the aggregate boundary:
* External callers cannot instantiate or modify `PurchaseOrderItem` directly; all item additions are mediated by `PurchaseOrder.AddItem(...)`.
* `PurchaseOrder` creates line items using its own currency, ensuring single-currency consistency across all items.
* `AddItem` handles quantity merging for duplicate products.
* The items collection is exposed to callers as a read-only list (`IReadOnlyList<PurchaseOrderItem>`).

### Consequences
* **Positive**: Domain invariants are strictly protected; invalid aggregate states are impossible.
* **Positive**: Consistent calculations for line totals and order totals.
* **Negative**: Line items cannot be manipulated independently of the aggregate root.

---

## ADR 003: Immutability and Value Semantics via `readonly record struct`

### Status
Accepted

### Context
Domain concepts such as `Money`, `Address`, `ProductId`, and `SupplierId` represent values without distinct mutable lifecycle identities. Using reference types (`class`) creates unnecessary heap allocations, garbage collection overhead, and mutable state risks. Furthermore, monetary arithmetic must prevent adding mismatched currencies.

### Decision
We implement all domain Value Objects as C# `readonly record struct`:
* **Zero Allocation**: Instances are allocated on the stack or inline within containing aggregates.
* **Structural Equality**: Equality is based on value fields rather than reference identity.
* **Invariant Enforcement**: Constructors validate arguments using standard .NET throw helpers (`ArgumentException.ThrowIfNullOrWhiteSpace`, `ArgumentOutOfRangeException.ThrowIfNegative`).
* **Operator Overloads**: `Money` defines explicit operators (`+`, `*`) enforcing currency parity before performing arithmetic.

### Consequences
* **Positive**: High performance and zero GC overhead for value objects.
* **Positive**: Enforces compile-time and runtime immutability.
* **Positive**: Prevents cross-currency calculation bugs.

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
