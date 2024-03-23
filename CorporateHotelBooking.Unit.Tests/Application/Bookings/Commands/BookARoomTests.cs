using AutoFixture;
using AutoFixture.Xunit2;
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
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;
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
        var command = CreateRandomCommand();
        _hotelRepositoryMock.Setup(x => x.Exists(command.HotelId)).Returns(false);

        // Act
        var result = _handler.Handle(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Hotel not found.");
    }

    [Fact]
    public void RoomTypeIsNotProvidedByTheHotel()
    {
        // Arrange
        var command = CreateRandomCommand();
        _hotelRepositoryMock.Setup(x => x.Exists(command.HotelId)).Returns(true);
        _roomRepositoryMock.Setup(x => x.ExistsRoomType(command.HotelId, command.RoomType)).Returns(false);

        // Act
        var result = _handler.Handle(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Room type not provided by the hotel.");
    }

    [Theory, AutoData]
    public void BookingIsNotAllowedByEmployeeBookingPolicy(int companyId)
    {
        // Arrange
        var command = CreateRandomCommand();
        _hotelRepositoryMock.Setup(x => x.Exists(command.HotelId)).Returns(true);
        _roomRepositoryMock.Setup(x => x.ExistsRoomType(command.HotelId, command.RoomType)).Returns(true);
        _employeeRepositoryMock.Setup(x => x.Get(command.EmployeeId))
            .Returns(new Employee(command.EmployeeId, companyId));
        _employeeBookingPolicyRepositoryMock.Setup(x => x.Exists(command.EmployeeId)).Returns(true);
        _employeeBookingPolicyRepositoryMock.Setup(x => x.Get(command.EmployeeId))
            .Returns(new EmployeeBookingPolicy(
                    command.EmployeeId,
                    new List<RoomType> { DifferentRoomTypeFrom(command.RoomType) }));
        _companyBookingPolicyRepositoryMock.Setup(x => x.Exists(companyId)).Returns(false);

        // Act
        Action act = () => _handler.Handle(command);

        // Assert
        act.Should().Throw<BookingNotAllowedException>();
    }

    [Theory, AutoData]
    public void BookingNotAllowedByCompanyBookingPolicy(int companyId)
    {
        // Arrange
        var command = CreateRandomCommand();
        _hotelRepositoryMock.Setup(x => x.Exists(command.HotelId)).Returns(true);
        _roomRepositoryMock.Setup(x => x.ExistsRoomType(command.HotelId, command.RoomType)).Returns(true);
        _employeeRepositoryMock.Setup(x => x.Get(command.EmployeeId))
            .Returns(new Employee(command.EmployeeId, companyId));
        _employeeBookingPolicyRepositoryMock.Setup(x => x.Exists(command.EmployeeId)).Returns(false);
        _companyBookingPolicyRepositoryMock.Setup(x => x.Exists(companyId)).Returns(true);
        _companyBookingPolicyRepositoryMock.Setup(x => x.Get(companyId))
            .Returns(new CompanyBookingPolicy(
                companyId,
                new List<RoomType> { DifferentRoomTypeFrom(command.RoomType) }));

        // Act
        Action act = () => _handler.Handle(command);

        // Assert
        act.Should().Throw<BookingNotAllowedException>();
    }

    [Theory, AutoData]
    public void NoRoomsOfThatTypeAvailableForTheRequestedPeriod(int companyId)
    {
        // Arrange
        var command = CreateRandomCommand();
        _hotelRepositoryMock.Setup(x => x.Exists(command.HotelId)).Returns(true);
        _roomRepositoryMock.Setup(x => x.ExistsRoomType(command.HotelId, command.RoomType)).Returns(true);
        _employeeRepositoryMock.Setup(x => x.Get(command.EmployeeId))
            .Returns(new Employee(command.EmployeeId, companyId));
        _employeeBookingPolicyRepositoryMock.Setup(x => x.Exists(command.EmployeeId)).Returns(false);
        _companyBookingPolicyRepositoryMock.Setup(x => x.Exists(companyId)).Returns(false);
        _roomRepositoryMock.Setup(x => x.GetCount(command.HotelId, command.RoomType)).Returns(1);
        _bookingRepositoryMock
            .Setup(x => x.GetCount(command.HotelId, command.RoomType, command.CheckInDate, command.CheckOutDate))
            .Returns(1);

        // Act
        var result = _handler.Handle(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("No rooms of that type available for the requested period.");
    }

    [Theory, AutoData]
    public void RoomIsBooked(int companyId, int bookingId)
    {
        // Arrange
        var command = CreateRandomCommand();
        _hotelRepositoryMock.Setup(x => x.Exists(command.HotelId)).Returns(true);
        _roomRepositoryMock.Setup(x => x.ExistsRoomType(command.HotelId, RoomType.Standard)).Returns(true);
        _employeeRepositoryMock.Setup(x => x.Get(command.EmployeeId))
            .Returns(new Employee(command.EmployeeId, companyId));
        _employeeBookingPolicyRepositoryMock.Setup(x => x.Exists(command.EmployeeId)).Returns(true);
        _employeeBookingPolicyRepositoryMock.Setup(x => x.Get(command.EmployeeId))
            .Returns(new EmployeeBookingPolicy(command.EmployeeId, new List<RoomType> { command.RoomType }));
        _companyBookingPolicyRepositoryMock.Setup(x => x.Exists(companyId)).Returns(false);
        _roomRepositoryMock.Setup(x => x.GetCount(command.HotelId, RoomType.Standard)).Returns(1);
        _bookingRepositoryMock
            .Setup(x => x.GetCount(command.HotelId, command.RoomType, command.CheckInDate, command.CheckOutDate))
            .Returns(0);
        _bookingRepositoryMock.Setup(x => x.Add(It.IsAny<Booking>()))
            .Returns(new Booking(
                bookingId,
                command.EmployeeId,
                command.HotelId,
                command.RoomType,
                command.CheckInDate,
                command.CheckOutDate));

        // Act
        var result = _handler.Handle(command);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(
            new NewBooking(
                bookingId,
                command.EmployeeId,
                command.HotelId,
                command.RoomType,
                command.CheckInDate,
                command.CheckOutDate));

        _bookingRepositoryMock.Verify(
            x => x.Add(new Booking(
                command.EmployeeId,
                command.HotelId,
                command.RoomType,
                command.CheckInDate,
                command.CheckOutDate)),
            Times.Once);
    }

    private static BookARoomCommand CreateRandomCommand()
    {
        var fixture = new Fixture().Customize(new DateOnlyFixtureCustomization());
        var checkInDate = fixture.Create<DateOnly>();

         return new BookARoomCommand(
            EmployeeId: fixture.Create<int>(),
            HotelId: fixture.Create<int>(),
            RoomType: fixture.Create<RoomType>(),
            CheckInDate: checkInDate,
            CheckOutDate: checkInDate.AddDays(fixture.Create<int>())
         );
    }

    private static RoomType DifferentRoomTypeFrom(RoomType roomType)
    {
        return roomType switch
        {
            RoomType.Standard => RoomType.JuniorSuite,
            RoomType.JuniorSuite => RoomType.MasterSuite,
            RoomType.MasterSuite => RoomType.Standard,
            _ => throw new ArgumentOutOfRangeException(nameof(roomType), roomType, null)
        };
    }
}