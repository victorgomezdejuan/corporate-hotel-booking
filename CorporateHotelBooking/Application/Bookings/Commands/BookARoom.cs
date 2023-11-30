using CorporateHotelBooking.Application.Rooms.Commands.SetRoom;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
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
    private readonly IEmployeeBookingPolicyRepository _employeeBookingPolicyRepository;
    private readonly ICompanyBookingPolicyRepository _companyBookingPolicyRepository;

    public BookARoomCommandHandler(
        IBookingRepository bookingRepository,
        IHotelRepository hotelRepository,
        IRoomRepository roomRepository,
        IEmployeeBookingPolicyRepository employeeBookingPolicyRepository,
        ICompanyBookingPolicyRepository companyBookingPolicyRepository)
    {
        _bookingRepository = bookingRepository;
        _hotelRepository = hotelRepository;
        _roomRepository = roomRepository;
        _employeeBookingPolicyRepository = employeeBookingPolicyRepository;
        _companyBookingPolicyRepository = companyBookingPolicyRepository;
    }

    public NewBooking Handle(BookARoomCommand command)
    {
        if (!_hotelRepository.Exists(command.HotelId))
        {
            throw new HotelNotFoundException(command.HotelId);
        }

        if (!_roomRepository.ExistsRoomType(command.HotelId, command.RoomType))
        {
            throw new RoomTypeNotProvidedByTheHotelException(command.HotelId, command.RoomType);
        }

        return null;
    }
}