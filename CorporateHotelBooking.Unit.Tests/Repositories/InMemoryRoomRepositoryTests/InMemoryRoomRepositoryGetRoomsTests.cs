using FluentAssertions;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class InMemoryRoomRepositoryGetRoomsTests
{
    [Fact]
    public void GetNoRooms()
    {
        var repository = new InMemoryRoomRepository();

        var rooms = repository.GetRooms(1);

        rooms.Should().BeEmpty();
    }

    [Fact]
    public void GetOneRoom()
    {
        var repository = new InMemoryRoomRepository();
        repository.AddRoom(new Room(1, 100, RoomType.Standard));

        var rooms = repository.GetRooms(1);

        rooms.Should().HaveCount(1);
        rooms.First().Number.Should().Be(100);
        rooms.First().Type.Should().Be(RoomType.Standard);
    }

    [Fact]
    public void GetMultipleRooms()
    {
        var repository = new InMemoryRoomRepository();
        repository.AddRoom(new Room(1, 100, RoomType.Standard));
        repository.AddRoom(new Room(1, 101, RoomType.JuniorSuite));

        var rooms = repository.GetRooms(1);

        rooms.Should().HaveCount(2);
        rooms.First().Number.Should().Be(100);
        rooms.First().Type.Should().Be(RoomType.Standard);
        rooms.Last().Number.Should().Be(101);
        rooms.Last().Type.Should().Be(RoomType.JuniorSuite);
    }
}