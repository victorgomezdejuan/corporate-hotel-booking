using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Repositories.Hotels;

public interface IHotelRepository
{
    void Add(Hotel hotel);

    Hotel GetHotel(int hotelId);

    bool Exists(int hotelId);
}