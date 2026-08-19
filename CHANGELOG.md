# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.2.0] - 2026-08-19

### Added
- Automated unit test suite (`Acme.OOProgramming.Tests`) using xUnit covering domain aggregates, value objects, and presentation extensions across `Shared`, `SupplyChain`, and `Procurement` bounded contexts.
- Dedicated `Currency` value object representing ISO 4217 3-letter alphabetic currency codes.
- C# 14 implicit extension declarations (`extension(T)`) for presentation formatting (`ConsoleFormatting`) across `Procurement` and `Shared` bounded contexts.
- Comprehensive XMLDoc documentation across all domain aggregates, value objects, presentation extensions, and automated unit test fixtures.
- Enabled `<GenerateDocumentationFile>true</GenerateDocumentationFile>` in `Acme.OOProgramming.csproj` to enforce XML documentation generation during compilation.
- Architecture Decision Records for presentation extension members (ADR 006) and currency modeling (ADR 007) in `docs/adrs.md`.
- Requirements Traceability Matrix (RTM) and enhanced user story acceptance criteria in `docs/user-stories.md`.

### Changed
- Standardized namespace and project hierarchy from legacy `ACME.OOP.*` and `ACME.OOP.SCM` to `Acme.OOProgramming.*` and `Acme.OOProgramming.SupplyChain`.
- Hardened `readonly record struct` value objects (`Money`, `Address`, `ProductId`, `SupplierId`, `Currency`) with C# 14 `field` keyword validation and parameterless constructor safety.
- Refactored `PurchaseOrderItem` as an entity with encapsulated quantity mutation (`IncreaseQuantity`) and price mismatch validation.
- Optimized `PurchaseOrder.Items` using a cached `ReadOnlyCollection<PurchaseOrderItem>` view to avoid repeated allocations.
- Updated `docs/class-diagram.puml` and `Program.cs` entry point to reflect modernized domain types and APIs.
- Modernized `README.md` with .NET 10 / C# 14 technical specifications, domain-driven design taxonomy, domain invariants, and a comprehensive documentation index.

## [1.1.0] - 2026-08-17

### Added
- Architecture Decision Records (ADRs) in `docs/adrs.md` documenting decisions on bounded contexts, aggregate invariants, value objects, UUIDv7, and `DateOnly` modeling (ADR 001–005).
- Support for UUIDv7 time-ordered identifier generation via `Guid.CreateVersion7()` in `ProductId.New()`.
- Operator overloads (`+`, `*`) for `Money` value object arithmetic.
- Support for `DateOnly` constructor and property in `PurchaseOrder`.

### Changed
- Refactored `Money`, `Address`, `ProductId`, and `SupplierId` into `readonly record struct` types for immutability, structural equality, and zero-allocation performance.
- Aligned `Supplier` aggregate root to use strongly-typed `SupplierId Id` instead of primitive `string Identifier`.
- Strengthened `Money` invariant to prevent adding amounts with mismatched currencies.
- Updated `PurchaseOrder.AddItem` to merge quantities when adding items for an existing `ProductId`.
- Replaced manual argument validation with standard .NET throw helpers (`ArgumentException.ThrowIfNullOrWhiteSpace`, `ArgumentOutOfRangeException.ThrowIfNegativeOrZero`).
- Updated `docs/class-diagram.puml` and `Program.cs` entry point to reflect modernized domain types and APIs.
- Modernized `README.md` with .NET 10 / C# 14 technical specifications, domain-driven design taxonomy, domain invariants, and a comprehensive documentation index.

## [1.0.0] - 2026-08-17

### Added
- Initial project setup demonstrating Domain-Driven Design (DDD) and OOP principles across Supply Chain Management (`ACME.OOP.SCM`) and Procurement (`ACME.OOP.Procurement`) bounded contexts.
- `Supplier` aggregate root and `SupplierId` value object in SCM context.
- `PurchaseOrder` aggregate root, `PurchaseOrderItem` entity, and `ProductId` value object in Procurement context.
- Shared kernel value objects: `Money` and `Address` in `ACME.OOP.Shared`.
- User stories documentation in `docs/user-stories.md`.
- PlantUML class diagram in `docs/class-diagram.puml`.
- Console demo application in `ACME.OOP/Program.cs`.
