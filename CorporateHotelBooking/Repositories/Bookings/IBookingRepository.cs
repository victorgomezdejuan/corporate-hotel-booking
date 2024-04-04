using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.ValueObjects;

namespace CorporateHotelBooking.Repositories.Bookings;

public interface IBookingRepository
{
    Booking Add(Booking booking);
    void DeleteByEmployee(int employeeId);
    Booking? Get(int id);
    int GetCount(int hotelId, RoomType roomType, BookingDateRange dateRange);
}