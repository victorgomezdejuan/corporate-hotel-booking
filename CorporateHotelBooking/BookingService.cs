using CorporateHotelBooking.Application.Bookings.Commands;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking;

public class BookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeBookingPolicyRepository _employeeBookingPolicyRepository;
    private readonly ICompanyBookingPolicyRepository _companyBookingPolicyRepository;

    public BookingService(
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


    public NewBooking Book(int employeeId, int hotelId, RoomType roomType, DateOnly checkInDate, DateOnly checkOutDate)
    {
        return new BookARoomCommandHandler(
            _bookingRepository,
            _hotelRepository,
            _roomRepository,
            _employeeRepository,
            _employeeBookingPolicyRepository,
            _companyBookingPolicyRepository)
            .Handle(new BookARoomCommand(employeeId, hotelId, roomType, checkInDate, checkOutDate));
    }
}

public record NewBooking(int Id, int EmployeeId, int HotelId, RoomType RoomType, DateOnly CheckInDate, DateOnly CheckOutDate);