using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Repositories.Bookings;

public class InMemoryBookingRepository : IBookingRepository
{
    private readonly List<Booking> _bookings;

    public InMemoryBookingRepository()
    {
        _bookings = new List<Booking>();
    }

    public Booking Add(Booking booking)
    {
        var newBooking = new Booking(
            id: _bookings.Count + 1,
            employeeId: booking.EmployeeId,
            hotelId: booking.HotelId,
            roomType: booking.RoomType,
            checkInDate: booking.CheckInDate,
            checkOutDate: booking.CheckOutDate
        );
        _bookings.Add(newBooking);

        return newBooking;
    }

    public Booking Get(int id)
    {
        var booking = _bookings.SingleOrDefault(b => b.Id == id);
        
        if (booking is not null)
        {
            return booking;
        }
        else
        {
            throw new BookingNotFoundException();
        }
    }

    public int GetBookingCount(int hotelId, RoomType roomType, DateOnly dateFrom, DateOnly dateTo)
    {
        return _bookings.Count(b => b.HotelId == hotelId && b.RoomType == roomType &&
            (
                (b.CheckInDate >= dateFrom && b.CheckInDate <= dateTo) ||
                (b.CheckOutDate >= dateFrom && b.CheckOutDate <= dateTo)
            )
        );
    
    }
}