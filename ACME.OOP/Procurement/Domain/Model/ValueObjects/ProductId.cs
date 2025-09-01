namespace ACME.OOP.Procurement.Domain.Model.ValueObjects;

/// <summary>
/// Represents a product identifier value object in the Procurement bounded context. 
/// </summary>
public record ProductId
{
    public Guid Id { get; init; }
    
    /// <summary>
    /// Creates a new instance of <see cref="ProductId"/>. 
    /// </summary>
    /// <param name="id">The product identifier, which must be a non-empty Guid object.</param>
    /// <exception cref="ArgumentException">Thrown when the identifier is null or empty.</exception>   
    public ProductId(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Product ID cannot be an empty GUID.", nameof(id));
        Id = id;
    }
    
    /// <summary>
    /// Creates a new instance of <see cref="ProductId"/>. 
    /// </summary>
    /// <returns>A new <see cref="ProductId"/> instance containing a <see cref="Guid"/> object.</returns> 
    public static ProductId New() => new(Guid.NewGuid());
    
    /// <summary>
    /// Returns a string representation of the product identifier. 
    /// </summary>
    /// <returns>A string representation of the product identifier.</returns>
    public override string ToString() => Id.ToString();
}