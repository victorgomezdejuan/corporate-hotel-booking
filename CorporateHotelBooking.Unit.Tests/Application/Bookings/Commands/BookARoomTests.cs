using CorporateHotelBooking.Application.Bookings.Commands;
using CorporateHotelBooking.Application.Rooms.Commands.SetRoom;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;
using CorporateHotelBooking.Unit.Tests.Helpers;
using FluentAssertions;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.Bookings.Commands;

public class BookARoomTests
{
    private readonly Mock<IBookingRepository> _bookingRepositoryMock;
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly Mock<IRoomRepository> _roomRepositoryMock;
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly Mock<IEmployeeBookingPolicyRepository> _employeeBookingPolicyRepositoryMock;
    private readonly Mock<ICompanyBookingPolicyRepository> _companyBookingPolicyRepositoryMock;
    private readonly BookARoomCommandHandler _handler;

    public BookARoomTests()
    {
        _bookingRepositoryMock = new Mock<IBookingRepository>();
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _roomRepositoryMock = new Mock<IRoomRepository>();
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        _employeeBookingPolicyRepositoryMock = new Mock<IEmployeeBookingPolicyRepository>();
        _companyBookingPolicyRepositoryMock = new Mock<ICompanyBookingPolicyRepository>();

        _handler = new BookARoomCommandHandler(
            _bookingRepositoryMock.Object,
            _hotelRepositoryMock.Object,
            _roomRepositoryMock.Object,
            _employeeRepositoryMock.Object,
            _employeeBookingPolicyRepositoryMock.Object,
            _companyBookingPolicyRepositoryMock.Object);
    }

    [Fact]
    public void HotelDoesNotExist()
    {
        // Arrange
        _hotelRepositoryMock.Setup(x => x.Exists(1)).Returns(false);

        var command = new BookARoomCommand
        (
            EmployeeId: 1,
            HotelId: 1,
            RoomType: RoomType.Standard,
            CheckInDate: DateUtils.Today(),
            CheckOutDate: DateUtils.Today().AddDays(1)
        );

        // Act
        Action act = () => _handler.Handle(command);

        // Assert
        act.Should().Throw<HotelNotFoundException>();
    }

    [Fact]
    public void RoomTypeIsNotProvidedByTheHotel()
    {
        // Arrange
        _hotelRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        _roomRepositoryMock.Setup(x => x.ExistsRoomType(1, RoomType.Standard)).Returns(false);

        var command = new BookARoomCommand
        (
            EmployeeId: 1,
            HotelId: 1,
            RoomType: RoomType.Standard,
            CheckInDate: DateUtils.Today(),
            CheckOutDate: DateUtils.Today().AddDays(1)
        );

        // Act
        Action act = () => _handler.Handle(command);

        // Assert
        act.Should().Throw<RoomTypeNotProvidedByTheHotelException>();
    }

    [Fact]
    public void BookingIsNotAllowedByEmployeeBookingPolicy()
    {
        // Arrange
        _hotelRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        _roomRepositoryMock.Setup(x => x.ExistsRoomType(1, RoomType.Standard)).Returns(true);
        _employeeRepositoryMock.Setup(x => x.GetEmployee(10)).Returns(new Employee(10, 100));
        _employeeBookingPolicyRepositoryMock.Setup(x => x.Exists(10)).Returns(true);
        _employeeBookingPolicyRepositoryMock.Setup(x => x.GetEmployeePolicy(10)).Returns(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.JuniorSuite }));
        _companyBookingPolicyRepositoryMock.Setup(x => x.Exists(100)).Returns(false);

        var command = new BookARoomCommand
        (
            EmployeeId: 10,
            HotelId: 1,
            RoomType: RoomType.Standard,
            CheckInDate: DateUtils.Today(),
            CheckOutDate: DateUtils.Today().AddDays(1)
        );

        // Act
        Action act = () => _handler.Handle(command);

        // Assert
        act.Should().Throw<BookingNotAllowedException>();
    }

    [Fact]
    public void BookingNotAllowedByCompanyBookingPolicy()
    {
        // Arrange
        _hotelRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        _roomRepositoryMock.Setup(x => x.ExistsRoomType(1, RoomType.Standard)).Returns(true);
        _employeeRepositoryMock.Setup(x => x.GetEmployee(10)).Returns(new Employee(10, 100));
        _employeeBookingPolicyRepositoryMock.Setup(x => x.Exists(10)).Returns(false);
        _companyBookingPolicyRepositoryMock.Setup(x => x.Exists(100)).Returns(true);
        _companyBookingPolicyRepositoryMock.Setup(x => x.Get(100)).Returns(new CompanyBookingPolicy(1, new List<RoomType> { RoomType.JuniorSuite }));

        var command = new BookARoomCommand
        (
            EmployeeId: 10,
            HotelId: 1,
            RoomType: RoomType.Standard,
            CheckInDate: DateUtils.Today(),
            CheckOutDate: DateUtils.Today().AddDays(1)
        );

        // Act
        Action act = () => _handler.Handle(command);

        // Assert
        act.Should().Throw<BookingNotAllowedException>();
    }

    [Fact]
    public void NoRoomsOfThatTypeAvailableForTheRequestedPeriod()
    {
        // Arrange
        _hotelRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        _roomRepositoryMock.Setup(x => x.ExistsRoomType(1, RoomType.Standard)).Returns(true);
        _employeeRepositoryMock.Setup(x => x.GetEmployee(10)).Returns(new Employee(10, 100));
        _employeeBookingPolicyRepositoryMock.Setup(x => x.Exists(10)).Returns(false);
        _companyBookingPolicyRepositoryMock.Setup(x => x.Exists(100)).Returns(false);
        _roomRepositoryMock.Setup(x => x.GetRoomCount(1, RoomType.Standard)).Returns(1);
        _bookingRepositoryMock.Setup(x => x.GetCount(1, RoomType.Standard, DateUtils.Today(), DateUtils.Today().AddDays(1))).Returns(1);

        var command = new BookARoomCommand
        (
            EmployeeId: 10,
            HotelId: 1,
            RoomType: RoomType.Standard,
            CheckInDate: DateUtils.Today(),
            CheckOutDate: DateUtils.Today().AddDays(1)
        );

        // Act
        Action act = () => _handler.Handle(command);

        // Assert
        act.Should().Throw<NoRoomsAvailableException>();
    }

    [Fact]
    public void RoomIsBooked()
    {
        // Arrange
        _hotelRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        _roomRepositoryMock.Setup(x => x.ExistsRoomType(1, RoomType.Standard)).Returns(true);
        _employeeRepositoryMock.Setup(x => x.GetEmployee(10)).Returns(new Employee(10, 100));
        _employeeBookingPolicyRepositoryMock.Setup(x => x.Exists(10)).Returns(true);
        _employeeBookingPolicyRepositoryMock.Setup(x => x.GetEmployeePolicy(10)).Returns(new EmployeeBookingPolicy(10, new List<RoomType> { RoomType.Standard }));
        _companyBookingPolicyRepositoryMock.Setup(x => x.Exists(100)).Returns(false);
        _roomRepositoryMock.Setup(x => x.GetRoomCount(1, RoomType.Standard)).Returns(1);
        _bookingRepositoryMock.Setup(x => x.GetCount(1, RoomType.Standard, DateUtils.Today(), DateUtils.Today().AddDays(1))).Returns(0);
        _bookingRepositoryMock.Setup(x => x.Add(It.IsAny<Booking>())).Returns(new Booking(1, 10, 1, RoomType.Standard, DateUtils.Today(), DateUtils.Today().AddDays(1)));

        var command = new BookARoomCommand
        (
            EmployeeId: 10,
            HotelId: 1,
            RoomType: RoomType.Standard,
            CheckInDate: DateUtils.Today(),
            CheckOutDate: DateUtils.Today().AddDays(1)
        );

        // Act
        NewBooking newBooking = _handler.Handle(command);

        // Assert
        _bookingRepositoryMock.Verify(
            x => x.Add(new Booking(10, 1, RoomType.Standard, DateUtils.Today(), DateUtils.Today().AddDays(1))),
            Times.Once);
        newBooking.Should().BeEquivalentTo(new NewBooking(1, 10, 1, RoomType.Standard, DateUtils.Today(), DateUtils.Today().AddDays(1)));
    }
}