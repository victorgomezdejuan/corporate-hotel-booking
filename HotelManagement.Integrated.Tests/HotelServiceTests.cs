using HotelManagement.Application;
using HotelManagement.Application.Hotels.Commands.AddHotel;
using HotelManagement.Repositories.Hotels;
using HotelManagement.Repositories.Rooms;

namespace HotelManagement.Integrated.Tests;

public class HotelServiceTests
{
    [Fact]
    public void AddNewHotel()
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

    [Fact]
    public void AddHotelWithExistingHotelId()
    {
        // Arrange
        var hotelRepository = new InMemoryHotelRepository();
        var roomRepository = new InMemoryRoomRepository();
        var hotelService = new HotelService(hotelRepository, roomRepository);
        hotelService.AddHotel(1, "Hotel 1");

        // Act
        void Act() => hotelService.AddHotel(1, "Hotel 2");

        // Assert
        Assert.Throws<HotelAlreadyExistsException>(Act);
    }
}