namespace ACME.OOP.SCM.Domain.Model.ValueObjects;

/// <summary>
/// Represents a supplier identifier value object in the Supply Chain Management (SCM) bounded context.
/// </summary>
public record SupplierId
{
    public string Identifier { get; init; }

    /// <summary>
    /// Creates a new instance of <see cref="SupplierId"/>. 
    /// </summary>
    /// <param name="identifier">The unique identifier for the supplier.</param>
    /// <exception cref="ArgumentException">Thrown when the identifier is null or empty.</exception>
    public SupplierId(string identifier)
    {
        if (string.IsNullOrWhiteSpace(identifier))
            throw new ArgumentException("Identifier cannot be null or empty.", nameof(identifier));
        Identifier = identifier;
    }
}