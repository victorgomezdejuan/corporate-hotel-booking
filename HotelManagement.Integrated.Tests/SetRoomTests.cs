using FluentAssertions;
using HotelManagement.Application;
using HotelManagement.Application.Rooms.Commands.SetRoom;
using HotelManagement.Domain;
using HotelManagement.Repositories.Hotels;
using HotelManagement.Repositories.Rooms;

namespace HotelManagement.Integrated.Tests;

public class SetRoomTests
{
    [Fact]
    public void AddNewRoom()
    {
        // Arrange
        var hotelRepository = new InMemoryHotelRepository();
        var roomRepository = new InMemoryRoomRepository();
        var hotelService = new HotelService(hotelRepository, roomRepository);
        hotelService.AddHotel(1, "Hotel 1");

        // Act
        hotelService.SetRoom(1, 101, RoomType.Single);

        // Assert
        var rooms = roomRepository.GetRooms(1);
        rooms.Should().HaveCount(1);
        rooms.Should().Contain(r => r.Number == 101 && r.Type == RoomType.Single);
    }

    [Fact]
    public void UpdateExistingRoom()
    {
        // Arrange
        var hotelRepository = new InMemoryHotelRepository();
        var roomRepository = new InMemoryRoomRepository();
        var hotelService = new HotelService(hotelRepository, roomRepository);
        hotelService.AddHotel(1, "Hotel 1");
        hotelService.SetRoom(1, 101, RoomType.Single);

        // Act
        hotelService.SetRoom(1, 101, RoomType.Double);

        // Assert
        var rooms = roomRepository.GetRooms(1);
        rooms.Should().HaveCount(1);
        rooms.Should().Contain(r => r.Number == 101 && r.Type == RoomType.Double);
    }

    [Fact]
    public void AddRoomAssignedToNonExistingHotel()
    {
        // Arrange
        var hotelRepository = new InMemoryHotelRepository();
        var roomRepository = new InMemoryRoomRepository();
        var hotelService = new HotelService(hotelRepository, roomRepository);

        // Act
        void Act() => hotelService.SetRoom(1, 101, RoomType.Single);

        // Assert
        Assert.Throws<HotelNotFoundException>(Act);
    }
}