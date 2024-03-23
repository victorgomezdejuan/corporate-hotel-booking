using CorporateHotelBooking.Application.Common;
using CorporateHotelBooking.Application.Common.Mappings;
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

    public Result<NewBooking> Handle(BookARoomCommand command)
    {
        bool hotelExist = DoesHotelExist(command.HotelId);
        if (!hotelExist)
        {
            return Result<NewBooking>.Failure("Hotel not found.");
        }

        bool roomTypeProvided = CheckRoomTypeExistanceInTheHotel(command);
        if (!roomTypeProvided)
        {
            return Result<NewBooking>.Failure("Room type not provided by the hotel.");
        }

        CheckBookingPolicyAllowance(command);

        bool roomsAvailable = AreThereRoomsAvailable(command);
        if (!roomsAvailable)
        {
            return Result<NewBooking>.Failure("No rooms of that type available for the requested period.");
        }
        
        Booking booking = _bookingRepository.Add(new Booking
        (
            employeeId: command.EmployeeId,
            hotelId: command.HotelId,
            roomType: command.RoomType,
            checkInDate: command.CheckInDate,
            checkOutDate: command.CheckOutDate
        ));

        return Result<NewBooking>.Success(booking.AsNewBooking());
    }

    private bool DoesHotelExist(int hotelId)
    {
        if (!_hotelRepository.Exists(hotelId))
        {
            return false;
        }

        return true;
    }

    private bool CheckRoomTypeExistanceInTheHotel(BookARoomCommand command)
    {
        if (!_roomRepository.ExistsRoomType(command.HotelId, command.RoomType))
        {
            return false;
        }

        return true;
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

    private bool AreThereRoomsAvailable(BookARoomCommand command)
    {
        if (
            _bookingRepository.GetCount(command.HotelId, command.RoomType, command.CheckInDate, command.CheckOutDate)
            >=
            _roomRepository.GetCount(command.HotelId, command.RoomType))
        {
            return false;
        }

        return true;
    }
}