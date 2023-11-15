using HotelManagement.Application;
using HotelManagement.Application.Hotels.Commands.AddHotel;
using HotelManagement.Repositories.Hotels;
using HotelManagement.Repositories.Rooms;

namespace HotelManagement.Integrated.Tests;

public class AddHotelTests
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly HotelService _hotelService;

    public AddHotelTests()
    {
        _hotelRepository = new InMemoryHotelRepository();
        _roomRepository = new InMemoryRoomRepository();
        _hotelService = new HotelService(_hotelRepository, _roomRepository);
    }

    [Fact]
    public void AddNewHotel()
    {
        // Act
        _hotelService.AddHotel(1, "Hotel 1");

        // Assert
        var hotel = _hotelService.FindHotelBy(1);
        Assert.Equal(1, hotel.Id);
        Assert.Equal("Hotel 1", hotel.Name);
    }

    [Fact]
    public void AddHotelWithExistingHotelId()
    {
        // Arrange
        _hotelService.AddHotel(1, "Hotel 1");

        // Act
        void Act() => _hotelService.AddHotel(1, "Hotel 2");

        // Assert
        Assert.Throws<HotelAlreadyExistsException>(Act);
    }
}