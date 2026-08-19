# User Stories

**Author**: Web Applications Developer Team  
**License**: See [LICENSE.md](../LICENSE.md) for details.

## Requirements Traceability Matrix (RTM)

| Story ID | User Story Title | Bounded Context(s) | Domain Components | Architectural Decisions |
| :--- | :--- | :--- | :--- | :--- |
| **US001** | Create a Purchase Order | `SupplyChain`, `Procurement`, `Shared` | `Supplier`, `SupplierId`, `Address`, `PurchaseOrder`, `Currency` | ADR 001, ADR 002, ADR 003, ADR 005, ADR 007 |
| **US002** | Add Items to Purchase Order | `Procurement`, `Shared` | `PurchaseOrder`, `PurchaseOrderItem`, `ProductId`, `Money`, `Currency` | ADR 001, ADR 002, ADR 003, ADR 004, ADR 007 |
| **US003** | Calculate Item Subtotal | `Procurement`, `Shared` | `PurchaseOrderItem`, `Money`, `Currency` | ADR 001, ADR 002, ADR 003 |
| **US004** | Calculate Order Total | `Procurement`, `Shared` | `PurchaseOrder`, `PurchaseOrderItem`, `Money`, `Currency` | ADR 001, ADR 002, ADR 003 |

---

## US001: Create a Purchase Order
As a procurement manager, I want to create a purchase order for a supplier so that I can order goods.

### Scenario: Successfully create a purchase order
- **Given** a supplier with code "SUP001", name "Supplier Inc.", and address "Supplier St, 123, SupplierCity, SC, 12345, United States"
- **When** the procurement manager creates a purchase order with order number "PO001" for supplier ID "SUP001" on March 29, 2025, in USD
- **Then** the purchase order is created with the correct order number, supplier ID, date, and currency

## US002: Add Items to Purchase Order
As a procurement manager, I want to add items to a purchase order so that I can specify what to order.

### Scenario: Successfully add an item
- **Given** a purchase order "PO001" for supplier ID "SUP001" in USD
- **When** the procurement manager adds an item with a newly generated time-ordered UUIDv7 product ID, quantity 10, and unit price amount 15.99
- **Then** the purchase order internally creates and contains the item with the correct product ID, quantity, and unit price of $15.99 USD

### Scenario: Add duplicate product with matching unit price merges quantity
- **Given** a purchase order "PO001" containing a product with ID "P001", quantity 10, and unit price $25.99 USD
- **When** the procurement manager adds the product with ID "P001", quantity 5, and matching unit price $25.99 USD
- **Then** the purchase order merges the quantity into the existing item resulting in quantity 15 and unit price $25.99 USD

### Scenario: Reject duplicate product with conflicting unit price
- **Given** a purchase order "PO001" containing a product with ID "P001" and unit price $25.99 USD
- **When** the procurement manager attempts to add the product with ID "P001" with a conflicting unit price $9.99 USD
- **Then** the operation is rejected with an error indicating conflicting unit prices

## US003: Calculate Purchase Order Item Subtotal
As a procurement manager, I want to calculate the subtotal of a purchase order item so that I can verify its cost.

### Scenario: Successfully calculate item subtotal
- **Given** a purchase order "PO001" with an item having a product ID, quantity 10, and unit price amount 25.99 in USD
- **When** the procurement manager requests the subtotal for the item
- **Then** the subtotal is calculated as $259.90 USD

## US004: Calculate Purchase Order Total
As a procurement manager, I want to calculate the total cost of a purchase order so that I know the overall expense.

### Scenario: Successfully calculate total
- **Given** a purchase order "PO001" with an item having a product ID, quantity 10, and unit price amount 25.99 in USD
- **When** the procurement manager requests the total
- **Then** the total is calculated as $259.90 USD