using HotelManagement.Domain;
using HotelManagement.Repositories;
using HotelManagement.Service;

namespace HotelManagement.Tests.InMemoryRoomRepositoryTests;

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

    [Fact]
    public void GetNonExistingRoom()
    {
        // Act
        void act() => _repository.GetRoom(1, 100);

        // Assert
        var exception = Assert.Throws<RoomNotFoundException>(act);
    }
}