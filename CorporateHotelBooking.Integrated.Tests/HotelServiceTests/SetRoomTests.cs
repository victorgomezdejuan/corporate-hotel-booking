using FluentAssertions;
using CorporateHotelBooking.Application.Rooms.Commands.SetRoom;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;
using CorporateHotelBooking.Services;
using AutoFixture.Xunit2;

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

    [Theory, AutoData]
    public void AddNewRoom(int hotelId, string hotelName, int roomNumber, RoomType roomType)
    {
        // Arrange
        _hotelService.AddHotel(hotelId, hotelName);

        // Act
        _hotelService.SetRoom(hotelId, roomNumber, roomType);

        // Assert
        var rooms = _roomRepository.GetMany(hotelId);
        rooms.Should().HaveCount(1);
        rooms.Should().Contain(r => r.Number == roomNumber && r.Type == roomType);
    }

    [Theory, AutoData]
    public void UpdateExistingRoom(int hotelId, string hotelName, int roomNumber)
    {
        // Arrange
        _hotelService.AddHotel(hotelId, hotelName);
        _hotelService.SetRoom(hotelId, roomNumber, RoomType.Standard);

        // Act
        _hotelService.SetRoom(hotelId, roomNumber, RoomType.JuniorSuite);

        // Assert
        var rooms = _roomRepository.GetMany(hotelId);
        rooms.Should().HaveCount(1);
        rooms.Should().Contain(r => r.Number == roomNumber && r.Type == RoomType.JuniorSuite);
    }

    [Theory, AutoData]
    public void AddRoomAssignedToNonExistingHotel(int hotelId, int roomNumber, RoomType roomType)
    {
        // Act
        void Act() => _hotelService.SetRoom(hotelId, roomNumber, roomType);

        // Assert
        Assert.Throws<HotelNotFoundException>(Act);
    }
}