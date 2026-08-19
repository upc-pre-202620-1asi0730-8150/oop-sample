using ACME.OOP.Shared.Domain.Model.ValueObjects;
using ACME.OOP.SupplyChain.Domain.Model.ValueObjects;

namespace ACME.OOP.SupplyChain.Domain.Model.Aggregates;

/// <summary>
/// Represents a supplier aggregate in the Supply Chain Management (SCM) bounded context.
/// </summary>
public class Supplier
{
    /// <summary>
    /// The unique identifier for the supplier.
    /// </summary>
    public SupplierId Id { get; }

    /// <summary>
    /// The name of the supplier.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the name is null or blank.</exception>
    public string Name
    {
        get;
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            field = value;
        }
    }

    /// <summary>
    /// The address of the supplier.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the address is an empty address.</exception>
    public Address Address
    {
        get;
        init
        {
            if (value == default)
                throw new ArgumentException("Address cannot be an empty address.", nameof(value));
            field = value;
        }
    }

    /// <summary>
    /// Creates a new instance of <see cref="Supplier"/>.
    /// </summary>
    /// <param name="id">The supplier identifier.</param>
    /// <param name="name">The supplier name.</param>
    /// <param name="address">The supplier address.</param>
    public Supplier(SupplierId id, string name, Address address)
    {
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
