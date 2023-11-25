using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Application.Bookings.Commands;

public record BookARoomCommand
(
    int EmployeeId,
    int HotelId,
    RoomType RoomType,
    DateTime CheckInDate,
    DateTime CheckOutDate
);

public class BookARoomCommandHandler
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomRepository _roomRepository;

    public BookARoomCommandHandler(IBookingRepository bookingRepository, IHotelRepository hotelRepository, IRoomRepository roomRepository)
    {
        _bookingRepository = bookingRepository;
        _hotelRepository = hotelRepository;
        _roomRepository = roomRepository;
    }

    public NewBooking Handle(BookARoomCommand command)
    {
        throw new NotImplementedException();
    }
}