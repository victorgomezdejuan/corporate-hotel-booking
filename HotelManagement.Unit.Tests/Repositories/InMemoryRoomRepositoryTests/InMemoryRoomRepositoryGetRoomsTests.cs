using FluentAssertions;
using HotelManagement.Domain;
using HotelManagement.Repositories.Rooms;

namespace HotelManagement.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

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
        repository.AddRoom(new Room(1, 100, RoomType.Double));

        var rooms = repository.GetRooms(1);

        rooms.Should().HaveCount(1);
        rooms.First().Number.Should().Be(100);
        rooms.First().Type.Should().Be(RoomType.Double);
    }
}