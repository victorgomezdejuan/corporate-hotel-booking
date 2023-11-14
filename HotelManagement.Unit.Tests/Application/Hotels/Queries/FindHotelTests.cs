using FluentAssertions;
using HotelManagement.Application;
using HotelManagement.Application.Hotels.Queries.FindHotel;
using HotelManagement.Domain;
using HotelManagement.Repositories.Hotels;
using Moq;

namespace HotelManagement.Unit.Tests.Application.Hotels.Queries;

public class FindHotelTests
{
    [Fact]
    public void Handle_ReturnsCorrectVmAndHotel()
    {
        // Arrange
        var expectedResult = new HotelDto(1, new List<RoomDto>() { new(100, RoomType.Double) });
        var hotelRepositoryMock = new Mock<IHotelRepository>();
        var retrievedHotel = new Hotel(1, "Hilton");
        retrievedHotel.AddRoom(new Room(1, 100, RoomType.Double));
        hotelRepositoryMock.Setup(x => x.GetHotelWithRooms(1)).Returns(retrievedHotel);
        var query = new FindHotelQuery { Id = 1 };
        var handler = new FindHotelQueryHandler(hotelRepositoryMock.Object);

        // Act
        HotelDto result = handler.Handle(query);

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