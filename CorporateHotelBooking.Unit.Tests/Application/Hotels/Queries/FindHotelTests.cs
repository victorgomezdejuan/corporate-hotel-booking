using FluentAssertions;
using HotelManagement.Application;
using HotelManagement.Application.Hotels.Queries.FindHotel;
using HotelManagement.Domain;
using HotelManagement.Repositories.Hotels;
using HotelManagement.Repositories.Rooms;
using Moq;

namespace HotelManagement.Unit.Tests.Application.Hotels.Queries;

public class FindHotelTests
{
    [Fact]
    public void Handle_ReturnsCorrectVmAndHotel()
    {
        // Arrange
        var hotelRepositoryMock = new Mock<IHotelRepository>();
        hotelRepositoryMock.Setup(x => x.GetHotel(1)).Returns(new Hotel(1, "Hilton"));
        var roomRepositoryMock = new Mock<IRoomRepository>();
        roomRepositoryMock.Setup(x => x.GetRooms(1)).Returns(new List<Room>() { new(1, 100, RoomType.Double) }.AsReadOnly());
        var handler = new FindHotelQueryHandler(hotelRepositoryMock.Object, roomRepositoryMock.Object);

        // Act
        HotelDto result = handler.Handle(new FindHotelQuery(1));

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<HotelDto>();
        result.Id.Should().Be(1);
        result.Rooms.Should().NotBeNull();
        result.Rooms.Should().HaveCount(1);
        result.Rooms.First().Number.Should().Be(100);
        result.Rooms.First().Type.Should().Be(RoomType.Double);
    }
}