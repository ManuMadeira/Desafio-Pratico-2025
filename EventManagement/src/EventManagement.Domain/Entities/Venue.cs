using System.Diagnostics.CodeAnalysis;
using EventManagement.Domain.Guards;

namespace EventManagement.Domain.Entities;

public class Venue
{
    public int VenueId { get; }
    public string Name { get; }
    public string Address { get; }
    public int Capacity { get; }
    
    private string? _description;
    private string? _parkingInfo;

    private static Venue? _default;

    public static Venue Default
    {
        get
        {
            _default ??= new Venue(0, "Evento Online", "Virtual", 1000);
            return _default;
        }
    }

    public Venue(int venueId, string name, string address, int capacity)
    {
        Guard.AgainstNegativeOrZero(venueId, nameof(venueId));
        Guard.AgainstNullOrWhiteSpace(name, nameof(name));
        Guard.AgainstNullOrWhiteSpace(address, nameof(address));
        Guard.AgainstNegativeOrZero(capacity, nameof(capacity));

        VenueId = venueId;
        Name = name.Trim();
        Address = address.Trim();
        Capacity = capacity;
    }

    public string? Description
    {
        get => _description;
        private set => _description = value;
    }

    [AllowNull]
    public string ParkingInfo
    {
        get => _parkingInfo ?? string.Empty;
        set => _parkingInfo = value;
    }

    public void SetDescription(string? description)
    {
        if (Guard.TryParseNonEmpty(description, out string result))
        {
            Description = result;
        }
        else
        {
            Description = null;
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is Venue venue && VenueId == venue.VenueId;
    }

    public override int GetHashCode()
    {
        return VenueId.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Name} - {Address} (Capacidade: {Capacity})";
    }
}