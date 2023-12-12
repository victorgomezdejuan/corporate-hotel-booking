using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class GetRoomCountTests
{
    [Fact]
    public void NoRooms()
    {
        // Arrange
        var repository = new InMemoryRoomRepository();

        // Act
        var count = repository.GetRoomCount(1, RoomType.Standard);

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void OneRoom()
    {
        // Arrange
        var repository = new InMemoryRoomRepository();
        repository.AddRoom(new Room(1, 100, RoomType.Standard));

        // Act
        var count = repository.GetRoomCount(1, RoomType.Standard);

        // Assert
        Assert.Equal(1, count);
    }

    [Fact]
    public void MultipleRooms()
    {
        // Arrange
        var repository = new InMemoryRoomRepository();
        repository.AddRoom(new Room(1, 100, RoomType.Standard));
        repository.AddRoom(new Room(1, 101, RoomType.Standard));
        repository.AddRoom(new Room(1, 102, RoomType.Standard));

        // Act
        var count = repository.GetRoomCount(1, RoomType.Standard);

        // Assert
        Assert.Equal(3, count);
    }
}