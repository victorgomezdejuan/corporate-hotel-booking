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
        if (!_hotelRepository.Exists(command.HotelId))
        {
            throw new HotelNotFoundException(command.HotelId);
        }

        if (!_roomRepository.ExistsRoomType(command.HotelId, command.RoomType))
        {
            throw new RoomTypeNotProvidedByTheHotelException(command.HotelId, command.RoomType);
        }

        BookingPolicy employeeBookingPolicy;
        if(_employeeBookingPolicyRepository.Exists(command.EmployeeId))
        {
            employeeBookingPolicy = _employeeBookingPolicyRepository.GetEmployeePolicy(command.EmployeeId);
        }
        else
        {
            employeeBookingPolicy = new NonApplicableBookingPolicy();
        }

        var employee = _employeeRepository.GetEmployee(command.EmployeeId);
        BookingPolicy companyBookingPolicy;
        if(_companyBookingPolicyRepository.Exists(employee.CompanyId))
        {
            companyBookingPolicy = _companyBookingPolicyRepository.GetCompanyPolicy(employee.CompanyId);
        }
        else
        {
            companyBookingPolicy = new NonApplicableBookingPolicy();
        }

        var bookingPolicy = new AggregatedBookingPolicy(employeeBookingPolicy, companyBookingPolicy);
        if (!bookingPolicy.BookingAllowed(command.RoomType))
        {
            throw new BookingNotAllowedException();
        }

        if(
            _bookingRepository.GetBookingCount(command.HotelId, command.RoomType, command.CheckInDate, command.CheckOutDate)
            >=
            _roomRepository.GetRoomCount(command.HotelId, command.RoomType))
        {
            throw new NoRoomsAvailableException();
        }
                
        return null;
    }
}