using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Repositories.Rooms;

public class InMemoryRoomRepository : IRoomRepository
{
    private readonly List<Room> _rooms;

    public InMemoryRoomRepository()
    {
        _rooms = new List<Room>();
    }

    public void Add(Room room)
    {
        _rooms.Add(room);
    }

    public void Update(Room room)
    {
        var existingRoom = Get(room.HotelId, room.Number);
        _rooms.Remove(existingRoom);
        _rooms.Add(room);
    }

    public bool ExistsRoomNumber(int hotelId, int number)
    {
        return _rooms.Any(r => r.HotelId == hotelId && r.Number == number);
    }

    public bool ExistsRoomType(int hotelId, RoomType roomType)
    {
        return _rooms.Any(r => r.HotelId == hotelId && r.Type == roomType);
    }

    public Room Get(int hotelId, int number)
    {
        return _rooms.Single(r => r.HotelId == hotelId && r.Number == number);
    }

    public int GetCount(int hotelId, RoomType roomType)
    {
        return _rooms.Count(r => r.HotelId == hotelId && r.Type == roomType);
    }

    public IReadOnlyCollection<Room> GetMany(int hotelId)
    {
        return _rooms.Where(r => r.HotelId == hotelId).ToList().AsReadOnly();
    }
}