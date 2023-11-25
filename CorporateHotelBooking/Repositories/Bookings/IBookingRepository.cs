using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Repositories.Bookings;

public interface IBookingRepository
{
    Booking GetBooking(int id);
}