using HotelService.Domain;
using HotelService.Repositories;

namespace HotelService.Tests.InMemoryRoomRepositoryTests;

public class InMemoryRoomRepositoryAddRoomTests
{
    private readonly InMemoryRoomRepository _repository;

    public InMemoryRoomRepositoryAddRoomTests()
    {
        _repository = new InMemoryRoomRepository();
    }

    [Fact]
    public void AddNewRoom()
    {
        // Arrange
        var roomToBeAdded = new Room(1, 100, RoomType.Single);

        // Act
        _repository.AddRoom(roomToBeAdded);

        // Assert
        var retrievedRoom = _repository.GetRoom(1, 100);
        Assert.Equal(roomToBeAdded, retrievedRoom);
    }

    [Fact]
    public void AddExistingRoom()
    {
        // Arrange
        var roomToBeAdded = new Room(1, 100, RoomType.Single);
        _repository.AddRoom(roomToBeAdded);

        // Act
        void act() => _repository.AddRoom(roomToBeAdded);

        // Assert
        var exception = Assert.Throws<RoomAlreadyExistsException>(act);
    }

    [Fact]
    public void AddExistingRoomWithDifferentData()
    {
        // Arrange
        var roomToBeAdded = new Room(1, 100, RoomType.Single);
        _repository.AddRoom(roomToBeAdded);

        // Act
        void act() => _repository.AddRoom(new Room(1, 100, RoomType.Double));

        // Assert
        var exception = Assert.Throws<RoomAlreadyExistsException>(act);
    }
}