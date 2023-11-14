using HotelManagement.Domain;
using HotelManagement.Repositories;

namespace HotelManagement.Unit.Tests.InMemoryRoomRepositoryTests;

public class InMemoryRoomRepositoryGetRoomTests
{
    private readonly InMemoryRoomRepository _repository;

    public InMemoryRoomRepositoryGetRoomTests()
    {
        _repository = new InMemoryRoomRepository();
    }

    [Fact]
    public void GetRoom()
    {
        // Arrange
        var existingRoom = new Room(1, 100, RoomType.Single);
        _repository.AddRoom(existingRoom);

        // Act
        var retrievedRoom = _repository.GetRoom(1, 100);

        // Assert
        Assert.Equal(existingRoom, retrievedRoom);
    }
}