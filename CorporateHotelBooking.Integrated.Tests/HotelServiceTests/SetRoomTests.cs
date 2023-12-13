using FluentAssertions;
using CorporateHotelBooking.Application.Rooms.Commands.SetRoom;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;
using CorporateHotelBooking.Services;

namespace CorporateHotelBooking.Integrated.Tests.HotelServiceTests;

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
        _hotelService.SetRoom(1, 101, RoomType.Standard);

        // Assert
        var rooms = _roomRepository.GetMany(1);
        rooms.Should().HaveCount(1);
        rooms.Should().Contain(r => r.Number == 101 && r.Type == RoomType.Standard);
    }

    [Fact]
    public void UpdateExistingRoom()
    {
        // Arrange
        _hotelService.AddHotel(1, "Hotel 1");
        _hotelService.SetRoom(1, 101, RoomType.Standard);

        // Act
        _hotelService.SetRoom(1, 101, RoomType.JuniorSuite);

        // Assert
        var rooms = _roomRepository.GetMany(1);
        rooms.Should().HaveCount(1);
        rooms.Should().Contain(r => r.Number == 101 && r.Type == RoomType.JuniorSuite);
    }

    [Fact]
    public void AddRoomAssignedToNonExistingHotel()
    {
        // Act
        void Act() => _hotelService.SetRoom(1, 101, RoomType.Standard);

        // Assert
        Assert.Throws<HotelNotFoundException>(Act);
    }
}