using AutoFixture.Xunit2;
using CorporateHotelBooking.Application.Bookings.Commands;
using CorporateHotelBooking.Application.Common;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Integrated.Tests.Helpers;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;
using CorporateHotelBooking.Services;
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
    private readonly BookingService _bookingService;

    public BookingServiceTests()
    {
        _bookingRepository = new InMemoryBookingRepository();
        _hotelRepository = new InMemoryHotelRepository();
        _roomRepository = new InMemoryRoomRepository();
        _employeeRepository = new InMemoryEmployeeRepository();
        _employeeBookingPolicyRepository = new InMemoryEmployeeBookingPolicyRepository();
        _companyBookingPolicyRepository = new InMemoryCompanyBookingPolicyRepository();
        _bookingService = new BookingService(
            _bookingRepository,
            _hotelRepository,
            _roomRepository,
            _employeeRepository,
            _employeeBookingPolicyRepository,
            _companyBookingPolicyRepository);
    }

    [Theory, AutoData]
    public void BookARoom(string hotelName, int roomNumber, int companyId)
    {
        // Arrange
        var booking = BookingFactory.CreateRandom();
        _hotelRepository.Add(new Hotel(booking.HotelId, hotelName));
        _roomRepository.Add(new Room(booking.HotelId, roomNumber, booking.RoomType));
        _employeeRepository.Add(new Employee(booking.EmployeeId, companyId));
        _companyBookingPolicyRepository
            .Add(new CompanyBookingPolicy(companyId, new List<RoomType> { booking.RoomType }));

        // Act
        var result = _bookingService.Book(
            booking.EmployeeId,
            booking.HotelId,
            booking.RoomType,
            booking.DateRange.CheckInDate,
            booking.DateRange.CheckOutDate);

        // Assert
        result.IsFailure.Should().BeFalse();

        result.Value!.EmployeeId.Should().Be(booking.EmployeeId);
        result.Value!.HotelId.Should().Be(booking.HotelId);
        result.Value!.RoomType.Should().Be(booking.RoomType);
        result.Value!.CheckInDate.Should().Be(booking.DateRange.CheckInDate);
        result.Value!.CheckOutDate.Should().Be(booking.DateRange.CheckOutDate);
        
        _bookingRepository.Get(1).Should().BeEquivalentTo(booking, options => options.Excluding(b => b.Id));
    }

    [Theory, AutoData]
    public void TryToBookARoomThatIsNotAvailableAtThatTime(
        string hotelName,
        int roomNumber,
        int companyId)
    {
        // Arrange
        var booking = BookingFactory.CreateRandom();
        _hotelRepository.Add(new Hotel(booking.HotelId, hotelName));
        _roomRepository.Add(new Room(booking.HotelId, roomNumber, RoomType.Standard));
        _employeeRepository.Add(new Employee(booking.EmployeeId, companyId));
        _companyBookingPolicyRepository.Add(
            new CompanyBookingPolicy(companyId, new List<RoomType> { RoomType.Standard }));
        _bookingRepository.Add(booking);

        // Act
        var result = _bookingService.Book(
            booking.EmployeeId,
            booking.HotelId,
            booking.RoomType,
            booking.DateRange.CheckInDate,
            booking.DateRange.CheckOutDate);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("No rooms of that type available for the requested period.");
    }
}