using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Repositories.Hotels;

public interface IHotelRepository
{
    void Add(Hotel hotel);

    Hotel Get(int hotelId);

    bool Exists(int hotelId);
}