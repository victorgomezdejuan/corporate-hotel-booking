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
        throw new NotImplementedException();
    }

    public void AddBooking(Booking booking)
    {
        _bookings.Add(booking);
    }

    public Booking GetBooking(int id)
    {
        throw new NotImplementedException();
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