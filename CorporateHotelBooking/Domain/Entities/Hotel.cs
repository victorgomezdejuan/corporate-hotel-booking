namespace CorporateHotelBooking.Domain.Entities;

public class Hotel
{
    public Hotel(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; }
    public string Name { get; }

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