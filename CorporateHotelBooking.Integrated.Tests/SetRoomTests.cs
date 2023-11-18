using FluentAssertions;
using CorporateHotelBooking.Application.Rooms.Commands.SetRoom;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Integrated.Tests;

public class SetRoomTests
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly HotelService _hotelService;

    public SetRoomTests()
    {
        _hotelRepository = new InMemoryHotelRepository();
        _roomRepository = new InMemoryRoomRepository();
        _hotelService = new HotelService(_hotelRepository, _roomRepository);
    }

    [Fact]
    public void AddNewRoom()
    {
        // Arrange
        _hotelService.AddHotel(1, "Hotel 1");

        // Act
        _hotelService.SetRoom(1, 101, RoomType.Single);

        // Assert
        var rooms = _roomRepository.GetRooms(1);
        rooms.Should().HaveCount(1);
        rooms.Should().Contain(r => r.Number == 101 && r.Type == RoomType.Single);
    }

    [Fact]
    public void UpdateExistingRoom()
    {
        // Arrange
        _hotelService.AddHotel(1, "Hotel 1");
        _hotelService.SetRoom(1, 101, RoomType.Single);

        // Act
        _hotelService.SetRoom(1, 101, RoomType.Double);

        // Assert
        var rooms = _roomRepository.GetRooms(1);
        rooms.Should().HaveCount(1);
        rooms.Should().Contain(r => r.Number == 101 && r.Type == RoomType.Double);
    }

    [Fact]
    public void AddRoomAssignedToNonExistingHotel()
    {
        // Act
        void Act() => _hotelService.SetRoom(1, 101, RoomType.Single);

        // Assert
        Assert.Throws<HotelNotFoundException>(Act);
    }
}