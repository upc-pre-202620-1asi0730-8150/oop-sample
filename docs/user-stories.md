# User Stories

**Author**: Web Applications Developer Team  
**License**: See [LICENSE.md](../LICENSE.md) for details.

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
- **When** the procurement manager adds an item with a newly generated product ID (Guid), quantity 10, and unit price amount 15.99
- **Then** the purchase order internally creates and contains the item with the correct product ID, quantity, and unit price of $15.99 USD

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