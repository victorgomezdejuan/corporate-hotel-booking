using System.Collections.ObjectModel;
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

    public bool ExistsRoomNumber(int hotelId, int number)
    {
        return _rooms.Any(r => r.HotelId == hotelId && r.Number == number);
    }

    public bool ExistsRoomType(int hotelId, RoomType roomType)
    {
        return _rooms.Any(r => r.HotelId == hotelId && r.Type == roomType);
    }

    public Room GetRoom(int hotelId, int number)
    {
        return _rooms.Single(r => r.HotelId == hotelId && r.Number == number);
    }

    public int GetRoomCount(int hotelId, RoomType roomType)
    {
        return _rooms.Count(r => r.HotelId == hotelId && r.Type == roomType);
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