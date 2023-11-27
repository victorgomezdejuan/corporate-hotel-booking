using CorporateHotelBooking.Application.Bookings.Commands;
using CorporateHotelBooking.Application.Rooms.Commands.SetRoom;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Bookings;
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

    public BookARoomTests()
    {
        _bookingRepositoryMock = new Mock<IBookingRepository>();
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _roomRepositoryMock = new Mock<IRoomRepository>();
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

        var handler = new BookARoomCommandHandler(_bookingRepositoryMock.Object, _hotelRepositoryMock.Object, _roomRepositoryMock.Object);

        // Act
        Action act = () => handler.Handle(command);

        // Assert
        act.Should().Throw<HotelNotFoundException>();
    }

    [Fact]
    public void RoomTypeIsNotProvidedByTheHotel()
    {
        // Arrange
        _hotelRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        _roomRepositoryMock.Setup(x => x.ExistsRoomType(RoomType.Standard)).Returns(false);

        var command = new BookARoomCommand
        (
            EmployeeId: 1,
            HotelId: 1,
            RoomType: RoomType.Standard,
            CheckInDate: DateUtils.Today(),
            CheckOutDate: DateUtils.Today().AddDays(1)
        );

        var handler = new BookARoomCommandHandler(_bookingRepositoryMock.Object, _hotelRepositoryMock.Object, _roomRepositoryMock.Object);

        // Act
        Action act = () => handler.Handle(command);

        // Assert
        act.Should().Throw<RoomTypeNotProvidedByTheHotelException>();
    }
}