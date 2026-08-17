namespace ACME.OOP.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Represents an international physical address value object.
/// </summary>
public readonly record struct Address
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
    /// <param name="street">The address street, which must not be null or blank.</param>
    /// <param name="number">The address number, which must not be null or blank.</param>
    /// <param name="city">The address city, which must not be null or blank.</param>
    /// <param name="stateOrRegion">The address state or region, which can be null.</param>
    /// <param name="postalCode">The address postal code, which must not be null or blank.</param>
    /// <param name="country">The address country, which must not be null or blank.</param>
    /// <exception cref="ArgumentException">Thrown when any required parameter is null or blank.</exception>
    public Address(string street, string number, string city, string? stateOrRegion, string postalCode, string country)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(street);
        ArgumentException.ThrowIfNullOrWhiteSpace(number);
        ArgumentException.ThrowIfNullOrWhiteSpace(city);
        ArgumentException.ThrowIfNullOrWhiteSpace(postalCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(country);

        Street = street;
        Number = number;
        City = city;
        StateOrRegion = stateOrRegion;
        PostalCode = postalCode;
        Country = country;
    }

    public override string ToString() => string.IsNullOrWhiteSpace(StateOrRegion)
        ? $"{Street}, {Number}, {City}, {PostalCode}, {Country}"
        : $"{Street}, {Number}, {City}, {StateOrRegion}, {PostalCode}, {Country}";
}
