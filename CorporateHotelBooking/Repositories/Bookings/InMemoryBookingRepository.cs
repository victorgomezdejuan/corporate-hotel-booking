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

    public void DeleteByEmployee(int employeeId)
    {
        _bookings.RemoveAll(b => b.EmployeeId == employeeId);
    }

    public Booking? Get(int id)
    {
        return _bookings.SingleOrDefault(b => b.Id == id);
    }

    public int GetCount(int hotelId, RoomType roomType, DateOnly dateFrom, DateOnly dateTo)
    {
        return _bookings.Count(b => b.HotelId == hotelId && b.RoomType == roomType &&
            (
                (b.CheckInDate >= dateFrom && b.CheckInDate <= dateTo) ||
                (b.CheckOutDate >= dateFrom && b.CheckOutDate <= dateTo)
            )
        );
    
    }
}