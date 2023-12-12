using System.Collections.ObjectModel;
using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Repositories.Rooms;

public interface IRoomRepository
{
    void Add(Room room);
    bool ExistsRoomNumber(int hotelId, int number);
    bool ExistsRoomType(int hotelId, RoomType roomType);
    Room Get(int hotelId, int number);
    int GetCount(int hotelId, RoomType roomType);
    ReadOnlyCollection<Room> GetMany(int hotelId);
    void UpdateRoom(Room room);
}