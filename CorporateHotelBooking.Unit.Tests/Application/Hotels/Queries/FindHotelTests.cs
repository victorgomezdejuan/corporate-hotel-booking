using FluentAssertions;
using CorporateHotelBooking.Application;
using CorporateHotelBooking.Application.Hotels.Queries.FindHotel;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;
using Moq;
using AutoFixture.Xunit2;

namespace CorporateHotelBooking.Unit.Tests.Application.Hotels.Queries;

public class FindHotelTests
{
    [Theory, AutoData]
    public void FindAnExistingHotel(int hotelId, string hotelName, int roomNumber, RoomType roomType)
    {
        // Arrange
        var hotelRepositoryMock = new Mock<IHotelRepository>();
        hotelRepositoryMock.Setup(x => x.Get(hotelId)).Returns(new Hotel(hotelId, hotelName));
        var roomRepositoryMock = new Mock<IRoomRepository>();
        roomRepositoryMock.Setup(x => x.GetMany(hotelId))
            .Returns(new List<Room>() { new(hotelId, roomNumber, roomType) }.AsReadOnly());
        var handler = new FindHotelQueryHandler(hotelRepositoryMock.Object, roomRepositoryMock.Object);

        // Act
        HotelDto result = handler.Handle(new FindHotelQuery(hotelId));

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<HotelDto>();
        result.Id.Should().Be(hotelId);
        result.Rooms.Should().NotBeNull();
        result.Rooms.Should().HaveCount(1);
        result.Rooms.First().Number.Should().Be(roomNumber);
        result.Rooms.First().Type.Should().Be(roomType);
    }

    [Theory, AutoData]
    public void FindANonExistingHotel(int hotelId)
    {
        // Arrange
        var hotelRepositoryMock = new Mock<IHotelRepository>();
        hotelRepositoryMock.Setup(x => x.Get(hotelId)).Returns((Hotel)null);
        var roomRepositoryMock = new Mock<IRoomRepository>();
        var handler = new FindHotelQueryHandler(hotelRepositoryMock.Object, roomRepositoryMock.Object);

        // Act
        HotelDto result = handler.Handle(new FindHotelQuery(hotelId));

        // Assert
        result.Should().BeNull();
    }
}