using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Repositories.Hotels;

public interface IHotelRepository
{
    void Add(Hotel hotel);
    bool Exists(int hotelId);
    Hotel Get(int hotelId);
}