namespace Acme.OOProgramming.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Represents an international physical address value object.
/// </summary>
public readonly record struct Address
{
    /// <summary>
    /// The street address.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the street is null, blank, or exceeds 100 characters.</exception>
    public string Street
    {
        get => field ?? string.Empty;
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > 100)
                throw new ArgumentException("Street cannot exceed 100 characters.", nameof(value));
            field = value;
        }
    }

    /// <summary>
    /// The street address number.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the number is null, blank, or exceeds 10 characters.</exception>
    public string Number
    {
        get => field ?? string.Empty;
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > 10)
                throw new ArgumentException("Number cannot exceed 10 characters.", nameof(value));
            field = value;
        }
    }

    /// <summary>
    /// The city.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the city is null, blank, or exceeds 100 characters.</exception>
    public string City
    {
        get => field ?? string.Empty;
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > 100)
                throw new ArgumentException("City cannot exceed 100 characters.", nameof(value));
            field = value;
        }
    }

    /// <summary>
    /// The state or region.
    /// </summary>
    public string? StateOrRegion { get; init; }

    /// <summary>
    /// The postal code.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the postal code is null, blank, or exceeds 20 characters.</exception>
    public string PostalCode
    {
        get => field ?? string.Empty;
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > 20)
                throw new ArgumentException("Postal code cannot exceed 20 characters.", nameof(value));
            field = value;
        }
    }

    /// <summary>
    /// The country.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the country is null, blank, or exceeds 100 characters.</exception>
    public string Country
    {
        get => field ?? string.Empty;
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > 100)
                throw new ArgumentException("Country cannot exceed 100 characters.", nameof(value));
            field = value;
        }
    }

    /// <summary>
    /// Prevents parameterless construction of <see cref="Address"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Always thrown because address components are required.</exception>
    public Address() => throw new InvalidOperationException("Address must be initialized with street, number, city, postal code, and country.");

    /// <summary>
    /// Creates a new instance of <see cref="Address"/>. 
    /// </summary>
    /// <param name="street">The address street, which must not be null, blank, or exceed 100 characters.</param>
    /// <param name="number">The address number, which must not be null, blank, or exceed 10 characters.</param>
    /// <param name="city">The address city, which must not be null, blank, or exceed 100 characters.</param>
    /// <param name="stateOrRegion">The address state or region, which can be null.</param>
    /// <param name="postalCode">The address postal code, which must not be null, blank, or exceed 20 characters.</param>
    /// <param name="country">The address country, which must not be null, blank, or exceed 100 characters.</param>
    public Address(string street, string number, string city, string? stateOrRegion, string postalCode, string country)
    {
        
        Street = street;
        Number = number;
        City = city;
        StateOrRegion = stateOrRegion;
        PostalCode = postalCode;
        Country = country;
    }

    /// <summary>
    /// Returns a string representation of the address.
    /// </summary>
    /// <returns>A string representation of the address, which may include the state or region if present.</returns>
    public override string ToString() => string.IsNullOrWhiteSpace(StateOrRegion)
        ? $"{Street}, {Number}, {City}, {PostalCode}, {Country}"
        : $"{Street}, {Number}, {City}, {StateOrRegion}, {PostalCode}, {Country}";
}
