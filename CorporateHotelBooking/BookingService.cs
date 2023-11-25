using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking;

public class BookingService
{
    private IBookingRepository _bookingRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomRepository _roomRepository;

    public BookingService(IBookingRepository bookingRepository, IHotelRepository hotelRepository, IRoomRepository roomRepository)
    {
        _bookingRepository = bookingRepository;
        _hotelRepository = hotelRepository;
        _roomRepository = roomRepository;
    }

    public NewBooking Book(int employeeId, int hotelId, RoomType roomType, DateOnly checkInDate, DateOnly checkOutDate)
    {
        throw new NotImplementedException();
    }
}

public record NewBooking(int Id, int EmployeeId, int HotelId, RoomType RoomType, DateOnly CheckInDate, DateOnly CheckOutDate);