namespace ACME.OOP.SCM.Domain.Model.ValueObjects;

/// <summary>
/// Represents a supplier identifier value object in the Supply Chain Management (SCM) bounded context.
/// </summary>
public readonly record struct SupplierId
{
    public string Identifier
    {
        get;
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            field = value;
        }
    }

    /// <summary>
    /// Creates a new instance of <see cref="SupplierId"/>. 
    /// </summary>
    /// <param name="identifier">The unique identifier for the supplier.</param>
    /// <exception cref="ArgumentException">Thrown when the identifier is null or empty.</exception>
    public SupplierId(string identifier) => Identifier = identifier;

    public override string ToString() => Identifier;
}
