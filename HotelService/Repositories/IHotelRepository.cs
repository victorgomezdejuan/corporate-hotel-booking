using HotelService.Domain;

namespace HotelService.Repositories;

public interface IHotelRepository
{
    void AddHotel(Hotel hotel);

    Hotel GetHotel(int hotelId);

    bool Exists(int hotelId);
}