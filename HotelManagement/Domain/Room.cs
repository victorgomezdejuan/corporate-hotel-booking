namespace HotelManagement.Domain;

public enum RoomType
{
    Single,
    Double,
    Suite
}

public class Room
{
    public int HotelId { get; }
    public int Number { get; }
    public RoomType Type { get; }

    public Room(int hotelId, int number, RoomType type)
    {
        HotelId = hotelId;
        Number = number;
        Type = type;
    }

    public Room UpdateType(RoomType roomType)
    {
        return new Room(HotelId, Number, roomType);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Room other)
        {
            return false;
        }

        return HotelId == other.HotelId && Number == other.Number && Type == other.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(HotelId, Number, Type);
    }
}