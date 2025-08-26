namespace ACME.OOP.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Represents an international physical address value object.
///
/// </summary>
public record Address
{
    public string Street { get; init; }
    public string Number { get; init; }
    public string City { get; init; }
    public string? StateOrRegion { get; init; }
    public string PostalCode { get; init; }
    public string Country { get; init; }
    
    /// <summary>
    /// Creates a new instance of <see cref="Address"/>. 
    /// </summary>
    /// <param name="street">the address street, which must not be null or blank.</param>
    /// <param name="number">the address number, which must not be null or blank.</param>
    /// <param name="city">the address city, which must not be null or blank.</param>
    /// <param name="stateOrRegion">the address state or region, which can be null.</param>
    /// <param name="postalCode">the address postal code, which must not be null or blank.</param>
    /// <param name="country">the address country, which must not be null or blank.</param>
    /// <exception cref="ArgumentException">thrown when any required parameter is null or blank.</exception>
    public Address(string street, string number, string city, string? stateOrRegion, string postalCode, string country)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street cannot be null or empty.", nameof(street));
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Number cannot be null or empty.", nameof(number));
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be null or empty.", nameof(city));
        if (string.IsNullOrWhiteSpace(postalCode))
            throw new ArgumentException("Postal code cannot be null or empty.", nameof(postalCode));
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country cannot be null or empty.", nameof(country));

        Street = street;
        Number = number;
        City = city;
        StateOrRegion = stateOrRegion;
        PostalCode = postalCode;
        Country = country;
    }
    
    public override string ToString() => $"{Street}, {Number}, {City}, {StateOrRegion}, {PostalCode}, {Country}";
    
     
}