using HotelManagement.Domain;

namespace HotelManagement.Repositories.Hotels;

public interface IHotelRepository
{
    void AddHotel(Hotel hotel);

    Hotel GetHotel(int hotelId);

    Hotel GetHotelWithRooms(int hotelId);

    bool Exists(int hotelId);
}