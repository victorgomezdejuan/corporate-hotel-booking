using HotelManagement.Application;
using HotelManagement.Repositories.Hotels;
using HotelManagement.Repositories.Rooms;

namespace HotelManagement.Integrated.Tests;

public class HotelServiceTests
{
    [Fact]
    public void AddHotel()
    {
        // Arrange
        var hotelRepository = new InMemoryHotelRepository();
        var roomRepository = new InMemoryRoomRepository();
        var hotelService = new HotelService(hotelRepository, roomRepository);

        // Act
        hotelService.AddHotel(1, "Hotel 1");

        // Assert
        var hotel = hotelService.FindHotel(1);
        Assert.Equal(1, hotel.Id);
        Assert.Equal("Hotel 1", hotel.Name);
    }
}