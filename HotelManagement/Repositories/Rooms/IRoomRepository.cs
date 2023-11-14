using System.Collections.ObjectModel;
using HotelManagement.Domain;

namespace HotelManagement.Repositories.Rooms;

public interface IRoomRepository
{
    void AddRoom(Room room);
    bool Exists(int hotelId, int number);
    Room GetRoom(int hotelId, int number);
    ReadOnlyCollection<Room> GetRooms(int hotelId);
    void UpdateRoom(Room room);
}