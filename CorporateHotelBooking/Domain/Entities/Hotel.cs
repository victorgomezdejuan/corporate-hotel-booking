using System.Collections.ObjectModel;

namespace CorporateHotelBooking.Domain.Entities;

public class Hotel
{
    private readonly List<Room> _rooms = new();

    public Hotel(int id, string name)
    {
        Id = id;
        Name = name;
        _rooms = new List<Room>();
    }

    public int Id { get; }
    public string Name { get; }

    public ReadOnlyCollection<Room> Rooms { get => _rooms.AsReadOnly(); }

    public void AddRoom(Room room)
    {
        _rooms.Add(room);
    }

    public override bool Equals(object? obj)
    {
        return obj is Hotel hotel &&
               Id == hotel.Id &&
               Name == hotel.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}