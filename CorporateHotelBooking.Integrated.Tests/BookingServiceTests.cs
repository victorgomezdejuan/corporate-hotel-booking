using CorporateHotelBooking.Application.Bookings.Commands;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests;

public class BookingServiceTests
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeBookingPolicyRepository _employeeBookingPolicyRepository;
    private readonly ICompanyBookingPolicyRepository _companyBookingPolicyRepository;

    public BookingServiceTests()
    {
        _bookingRepository = new InMemoryBookingRepository();
        _hotelRepository = new InMemoryHotelRepository();
        _roomRepository = new InMemoryRoomRepository();
        _employeeRepository = new InMemoryEmployeeRepository();
        _employeeBookingPolicyRepository = new InMemoryEmployeeBookingPolicyRepository();
        _companyBookingPolicyRepository = new InMemoryCompanyBookingPolicyRepository();
    }

    [Fact]
    public void BookARoom()
    {
        // Arrange
        var expectedBooking = new Booking
        (
            id: 1,
            employeeId: 2,
            hotelId: 3,
            roomType: RoomType.Standard,
            checkInDate: DateOnly.FromDateTime(DateTime.Now),
            checkOutDate: DateOnly.FromDateTime(DateTime.Now.AddDays(1))
        );
        _hotelRepository.AddHotel(new Hotel(3, "Hotel 3"));
        _roomRepository.AddRoom(new Room(3, 100, RoomType.Standard));
        _employeeRepository.AddEmployee(new Employee(2, 4));
        _companyBookingPolicyRepository.AddCompanyPolicy(new CompanyBookingPolicy(4, new List<RoomType> { RoomType.Standard }));
        var bookingService = new BookingService(
            _bookingRepository,
            _hotelRepository,
            _roomRepository,
            _employeeRepository,
            _employeeBookingPolicyRepository,
            _companyBookingPolicyRepository);

        // Act
        var booking = bookingService.Book(2, 3, RoomType.Standard, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1)));

        // Assert
        booking.Should().BeEquivalentTo(expectedBooking);
        _bookingRepository.Get(1).Should().Be(expectedBooking);
    }

    [Fact]
    public void TryToBookARoomThatIsNotAvailableAtThatTime()
    {
        // Arrange
        _hotelRepository.AddHotel(new Hotel(3, "Hotel 3"));
        _roomRepository.AddRoom(new Room(3, 100, RoomType.Standard));
        _employeeRepository.AddEmployee(new Employee(2, 4));
        _companyBookingPolicyRepository.AddCompanyPolicy(new CompanyBookingPolicy(4, new List<RoomType> { RoomType.Standard }));
        _bookingRepository.Add(new Booking(2, 3, RoomType.Standard, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1))));
        var bookingService = new BookingService(
            _bookingRepository,
            _hotelRepository,
            _roomRepository,
            _employeeRepository,
            _employeeBookingPolicyRepository,
            _companyBookingPolicyRepository);

        // Act
        Action action = () => bookingService.Book(2, 3, RoomType.Standard, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1)));

        // Assert
        action.Should().Throw<NoRoomsAvailableException>();
    }
}