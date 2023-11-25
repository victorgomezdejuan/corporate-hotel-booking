using System.Collections.ObjectModel;
using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Repositories.Rooms;

public class InMemoryRoomRepository : IRoomRepository
{
    private readonly List<Room> _rooms;

    public InMemoryRoomRepository()
    {
        _rooms = new List<Room>();
    }

    public void AddRoom(Room room)
    {
        _rooms.Add(room);
    }

    public bool ExistsRoomType(int hotelId, int number)
    {
        return _rooms.Any(r => r.HotelId == hotelId && r.Number == number);
    }

    public bool ExistsRoomType(RoomType standard)
    {
        return _rooms.Any(r => r.Type == standard);
    }

    public Room GetRoom(int hotelId, int number)
    {
        return _rooms.Single(r => r.HotelId == hotelId && r.Number == number);
    }

    public ReadOnlyCollection<Room> GetRooms(int hotelId)
    {
        return _rooms.Where(r => r.HotelId == hotelId).ToList().AsReadOnly();
    }

    public void UpdateRoom(Room room)
    {
        var existingRoom = GetRoom(room.HotelId, room.Number);
        _rooms.Remove(existingRoom);
        _rooms.Add(room);
    }
}