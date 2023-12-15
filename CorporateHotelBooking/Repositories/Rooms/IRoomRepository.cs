using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Repositories.Rooms;

public interface IRoomRepository
{
    void Add(Room room);
    void Update(Room room);
    bool ExistsRoomNumber(int hotelId, int number);
    bool ExistsRoomType(int hotelId, RoomType roomType);
    Room Get(int hotelId, int number);
    int GetCount(int hotelId, RoomType roomType);
    IReadOnlyCollection<Room> GetMany(int hotelId);
}