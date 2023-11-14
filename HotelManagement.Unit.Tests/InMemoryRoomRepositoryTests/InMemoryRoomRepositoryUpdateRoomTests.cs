using HotelManagement.Domain;
using HotelManagement.Repositories.Rooms;

namespace HotelManagement.Unit.Tests.InMemoryRoomRepositoryTests;

public class InMemoryRoomRepositoryUpdateRoomTests
{
    private readonly InMemoryRoomRepository _repository;

    public InMemoryRoomRepositoryUpdateRoomTests()
    {
        _repository = new InMemoryRoomRepository();
    }

    [Fact]
    public void UpdateExistingRoom()
    {
        // Arrange
        var existingRoom = new Room(1, 100, RoomType.Single);
        _repository.AddRoom(existingRoom);
        var updatedRoom = new Room(1, 100, RoomType.Double);

        // Act
        _repository.UpdateRoom(updatedRoom);

        // Assert
        var retrievedRoom = _repository.GetRoom(1, 100);
        Assert.Equal(updatedRoom, retrievedRoom);
    }
}