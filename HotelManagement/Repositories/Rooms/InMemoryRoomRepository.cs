using HotelManagement.Domain;

namespace HotelManagement.Repositories.Rooms;

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

    public bool Exists(int hotelId, int number)
    {
        return _rooms.Any(r => r.HotelId == hotelId && r.Number == number);
    }

    public Room GetRoom(int hotelId, int number)
    {
        return _rooms.Single(r => r.HotelId == hotelId && r.Number == number);
    }

    public void UpdateRoom(Room room)
    {
        var existingRoom = GetRoom(room.HotelId, room.Number);
        _rooms.Remove(existingRoom);
        _rooms.Add(room);
    }
}