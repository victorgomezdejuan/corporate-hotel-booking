using HotelService.Domain;

namespace HotelService.Repositories;

public interface IRoomRepository
{
    void AddRoom(Room room);
    bool Exists(int hotelId, int number);
    Room GetRoom(int hotelId, int number);
    void UpdateRoom(Room room);
}