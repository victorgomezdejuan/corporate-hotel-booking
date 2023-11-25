using CorporateHotelBooking.Application.Bookings.Commands;
using CorporateHotelBooking.Application.Rooms.Commands.SetRoom;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;
using CorporateHotelBooking.Unit.Tests.Helpers;
using FluentAssertions;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.Bookings.Commands;

public class BookARoomTests
{
    [Fact]
    public void HotelDoesNotExist()
    {
        // Arrange
        var bookingRepositoryMock = new Mock<IBookingRepository>();
        var hotelRepositoryMock = new Mock<IHotelRepository>();
        hotelRepositoryMock.Setup(x => x.Exists(1)).Returns(false);
        var roomRepositoryMock = new Mock<IRoomRepository>();

        var command = new BookARoomCommand
        (
            EmployeeId: 1,
            HotelId: 1,
            RoomType: RoomType.Standard,
            CheckInDate: DateUtils.Today(),
            CheckOutDate: DateUtils.Today().AddDays(1)
        );

        var handler = new BookARoomCommandHandler(bookingRepositoryMock.Object, hotelRepositoryMock.Object, roomRepositoryMock.Object);

        // Act
        Action act = () => handler.Handle(command);

        // Assert
        act.Should().Throw<HotelNotFoundException>();
    }

    [Fact]
    public void RoomTypeIsNotProvidedByTheHotel()
    {
        // Arrange
        var bookingRepositoryMock = new Mock<IBookingRepository>();
        var hotelRepositoryMock = new Mock<IHotelRepository>();
        hotelRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        var roomRepositoryMock = new Mock<IRoomRepository>();
        roomRepositoryMock.Setup(x => x.ExistsRoomType(RoomType.Standard)).Returns(false);

        var command = new BookARoomCommand
        (
            EmployeeId: 1,
            HotelId: 1,
            RoomType: RoomType.Standard,
            CheckInDate: DateUtils.Today(),
            CheckOutDate: DateUtils.Today().AddDays(1)
        );

        var handler = new BookARoomCommandHandler(bookingRepositoryMock.Object, hotelRepositoryMock.Object, roomRepositoryMock.Object);

        // Act
        Action act = () => handler.Handle(command);

        // Assert
        act.Should().Throw<RoomTypeNotProvidedByTheHotelException>();
    }
}