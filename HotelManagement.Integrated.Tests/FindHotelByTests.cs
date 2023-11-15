using FluentAssertions;
using HotelManagement.Application;
using HotelManagement.Domain;
using HotelManagement.Repositories.Hotels;
using HotelManagement.Repositories.Rooms;

namespace HotelManagement.Integrated.Tests;

public class FindHotelByTests
{
    [Fact]
    public void FindAHotel()
    {
        // Arrange
        var hotelRepository = new InMemoryHotelRepository();
        var roomRepository = new InMemoryRoomRepository();
        var hotelService = new HotelService(hotelRepository, roomRepository);
        hotelService.AddHotel(1, "Hotel 1");
        hotelService.SetRoom(1, 101, RoomType.Single);
        hotelService.SetRoom(1, 102, RoomType.Double);

        // Act
        var hotel = hotelService.FindHotelBy(1);

        // Assert
        hotel.Id.Should().Be(1);
        hotel.Name.Should().Be("Hotel 1");
        hotel.Rooms.Should().HaveCount(2);
        hotel.Rooms.Should().Contain(r => r.Number == 101 && r.Type == RoomType.Single);
        hotel.Rooms.Should().Contain(r => r.Number == 102 && r.Type == RoomType.Double);
    }
}