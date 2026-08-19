namespace Acme.OOProgramming.Procurement.Domain.Model.ValueObjects;

/// <summary>
/// Represents a product identifier value object in the Procurement bounded context. 
/// </summary>
public readonly record struct ProductId
{
    /// <summary>
    /// The unique identifier for the product.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the identifier is an empty GUID.</exception>
    public Guid Id
    {
        get;
        init
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Product ID cannot be an empty GUID.", nameof(value));
            field = value;
        }
    }

    /// <summary>
    /// Prevents parameterless construction of <see cref="ProductId"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Always thrown because a valid GUID is required.</exception>
    public ProductId() => throw new InvalidOperationException("ProductId must be initialized with a non-empty GUID.");

    /// <summary>
    /// Creates a new instance of <see cref="ProductId"/>. 
    /// </summary>
    /// <param name="id">The product identifier, which must be a non-empty Guid object.</param>
    public ProductId(Guid id) => Id = id;

    /// <summary>
    /// Creates a new instance of <see cref="ProductId"/> using a time-ordered UUIDv7. 
    /// </summary>
    /// <returns>A new <see cref="ProductId"/> instance containing a version 7 <see cref="Guid"/>.</returns> 
    public static ProductId New() => new(Guid.CreateVersion7());

    /// <summary>
    /// Returns a string representation of the product identifier. 
    /// </summary>
    /// <returns>A string representation of the product identifier.</returns>
    public override string ToString() => Id.ToString();
}
