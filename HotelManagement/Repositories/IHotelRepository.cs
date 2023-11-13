using HotelManagement.Domain;

namespace HotelManagement.Repositories;

public interface IHotelRepository
{
    void AddHotel(Hotel hotel);

    Hotel GetHotel(int hotelId);

    bool Exists(int hotelId);
}