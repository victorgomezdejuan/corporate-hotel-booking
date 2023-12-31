using CorporateHotelBooking.Application.Rooms.Commands.SetRoom;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
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
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeBookingPolicyRepository _employeeBookingPolicyRepository;
    private readonly ICompanyBookingPolicyRepository _companyBookingPolicyRepository;

    public BookARoomCommandHandler(
        IBookingRepository bookingRepository,
        IHotelRepository hotelRepository,
        IRoomRepository roomRepository,
        IEmployeeRepository employeeRepository,
        IEmployeeBookingPolicyRepository employeeBookingPolicyRepository,
        ICompanyBookingPolicyRepository companyBookingPolicyRepository)
    {
        _bookingRepository = bookingRepository;
        _hotelRepository = hotelRepository;
        _roomRepository = roomRepository;
        _employeeRepository = employeeRepository;
        _employeeBookingPolicyRepository = employeeBookingPolicyRepository;
        _companyBookingPolicyRepository = companyBookingPolicyRepository;
    }

    public NewBooking Handle(BookARoomCommand command)
    {
        CheckHotelExistance(command.HotelId);
        CheckRoomTypeExistanceInTheHotel(command);
        CheckBookingPolicyAllowance(command);
        CheckRoomAvailability(command);
        
        Booking booking = _bookingRepository.Add(new Booking
        (
            employeeId: command.EmployeeId,
            hotelId: command.HotelId,
            roomType: command.RoomType,
            checkInDate: command.CheckInDate,
            checkOutDate: command.CheckOutDate
        ));

        return new NewBooking(booking.Id!.Value, booking.EmployeeId, booking.HotelId, booking.RoomType, booking.CheckInDate, booking.CheckOutDate);
    }

    private void CheckHotelExistance(int hotelId)
    {
        if (!_hotelRepository.Exists(hotelId))
        {
            throw new HotelNotFoundException(hotelId);
        }
    }

    private void CheckRoomTypeExistanceInTheHotel(BookARoomCommand command)
    {
        if (!_roomRepository.ExistsRoomType(command.HotelId, command.RoomType))
        {
            throw new RoomTypeNotProvidedByTheHotelException(command.HotelId, command.RoomType);
        }
    }

    private void CheckBookingPolicyAllowance(BookARoomCommand command)
    {
        BookingPolicy employeeBookingPolicy = GetEmployeeBookingPolicy(command.EmployeeId);
        BookingPolicy companyBookingPolicy = GetCompanyBookingPolicy(command);

        var bookingPolicy = new AggregatedBookingPolicy(employeeBookingPolicy, companyBookingPolicy);
        if (!bookingPolicy.BookingAllowed(command.RoomType))
        {
            throw new BookingNotAllowedException();
        }
    }

    private BookingPolicy GetEmployeeBookingPolicy(int employeeId)
    {
        BookingPolicy employeeBookingPolicy;
        if (_employeeBookingPolicyRepository.Exists(employeeId))
        {
            employeeBookingPolicy = _employeeBookingPolicyRepository.Get(employeeId);
        }
        else
        {
            employeeBookingPolicy = new NonApplicableBookingPolicy();
        }

        return employeeBookingPolicy;
    }

    private BookingPolicy GetCompanyBookingPolicy(BookARoomCommand command)
    {
        BookingPolicy companyBookingPolicy;
        var employee = _employeeRepository.Get(command.EmployeeId);
        if (_companyBookingPolicyRepository.Exists(employee.CompanyId))
        {
            companyBookingPolicy = _companyBookingPolicyRepository.Get(employee.CompanyId);
        }
        else
        {
            companyBookingPolicy = new NonApplicableBookingPolicy();
        }

        return companyBookingPolicy;
    }

    private void CheckRoomAvailability(BookARoomCommand command)
    {
        if (
            _bookingRepository.GetCount(command.HotelId, command.RoomType, command.CheckInDate, command.CheckOutDate)
            >=
            _roomRepository.GetCount(command.HotelId, command.RoomType))
        {
            throw new NoRoomsAvailableException();
        }
    }
}