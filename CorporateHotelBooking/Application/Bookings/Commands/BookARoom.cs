using CorporateHotelBooking.Application.Rooms.Commands.SetRoom;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Application.Bookings.Commands;

public record BookARoomCommand
(
    int EmployeeId,
    int HotelId,
    RoomType RoomType,
    DateOnly CheckInDate,
    DateOnly CheckOutDate
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
        if (!_hotelRepository.Exists(command.HotelId))
        {
            throw new HotelNotFoundException(command.HotelId);
        }

        if (!_roomRepository.ExistsRoomType(command.RoomType))
        {
            throw new RoomTypeNotProvidedByTheHotelException(command.HotelId, command.RoomType);
        }

        return null;
    }
}