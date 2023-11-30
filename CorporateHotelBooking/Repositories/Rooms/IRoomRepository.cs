using System.Collections.ObjectModel;
using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Repositories.Rooms;

public interface IRoomRepository
{
    void AddRoom(Room room);
    bool ExistsRoomNumber(int hotelId, int number);
    bool ExistsRoomType(int hotelId, RoomType roomType);
    Room GetRoom(int hotelId, int number);
    ReadOnlyCollection<Room> GetRooms(int hotelId);
    void UpdateRoom(Room room);
}