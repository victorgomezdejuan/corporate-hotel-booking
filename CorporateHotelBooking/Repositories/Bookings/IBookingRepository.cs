using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Repositories.Bookings;

public interface IBookingRepository
{
    Booking GetBooking(int id);
    int GetBookingCount(int hotelId, RoomType roomType, DateOnly dateFrom, DateOnly dateTo);
}