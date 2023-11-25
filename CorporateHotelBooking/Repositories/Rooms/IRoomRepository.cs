using System.Collections.ObjectModel;
using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Repositories.Rooms;

public interface IRoomRepository
{
    void AddRoom(Room room);
    bool ExistsRoomType(int hotelId, int number);
    Room GetRoom(int hotelId, int number);
    ReadOnlyCollection<Room> GetRooms(int hotelId);
    void UpdateRoom(Room room);
}