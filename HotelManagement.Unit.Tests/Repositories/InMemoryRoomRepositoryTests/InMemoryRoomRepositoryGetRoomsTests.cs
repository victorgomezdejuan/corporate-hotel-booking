using FluentAssertions;
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
}