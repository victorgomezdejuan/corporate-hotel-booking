using CorporateHotelBooking.Application.Bookings.Commands;
using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Application.Common.Mappings;

public static class BookingExtensions
{
    public static NewBooking AsNewBooking(this Booking booking)
    {
        return new NewBooking(
            booking.Id!.Value,
            booking.EmployeeId,
            booking.HotelId,
            booking.RoomType,
            booking.DateRange.CheckInDate,
            booking.DateRange.CheckOutDate);
    }
}