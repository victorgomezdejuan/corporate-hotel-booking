using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Repositories.Bookings;

public interface IBookingRepository
{
    Booking Add(Booking booking);
    void DeleteByEmployeeId(int employeeId);
    Booking Get(int id);
    int GetCount(int hotelId, RoomType roomType, DateOnly dateFrom, DateOnly dateTo);
}