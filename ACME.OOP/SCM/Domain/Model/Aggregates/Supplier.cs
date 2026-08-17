using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

namespace ACME.OOP.SCM.Domain.Model.Aggregates;

/// <summary>
/// Represents a supplier aggregate in the Supply Chain Management (SCM) bounded context.
/// </summary>
public class Supplier
{
    public SupplierId Id { get; }
    public string Name { get; }
    public Address Address { get; }

    /// <summary>
    /// Creates a new instance of <see cref="Supplier"/>.
    /// </summary>
    /// <param name="id">The supplier identifier.</param>
    /// <param name="name">The supplier name.</param>
    /// <param name="address">The supplier address.</param>
    public Supplier(SupplierId id, string name, Address address)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Id = id;
        Name = name;
        Address = address;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Supplier"/> with a string identifier.
    /// </summary>
    /// <param name="identifier">The supplier identifier string.</param>
    /// <param name="name">The supplier name.</param>
    /// <param name="address">The supplier address.</param>
    public Supplier(string identifier, string name, Address address)
        : this(new SupplierId(identifier), name, address)
    {
    }
}
