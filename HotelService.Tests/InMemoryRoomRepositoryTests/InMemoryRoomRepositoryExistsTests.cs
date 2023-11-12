using HotelService.Domain;

namespace HotelService.Tests.InMemoryRoomRepositoryTests;

public class InMemoryRoomRepositoryExistsTests
{
    private readonly InMemoryRoomRepository _repository;

    public InMemoryRoomRepositoryExistsTests()
    {
        _repository = new InMemoryRoomRepository();
    }

    [Fact]
    public void RoomExists()
    {
        // Arrange
        _repository.AddRoom(new Room(1, 100, RoomType.Single));

        // Act
        var exists = _repository.Exists(1, 100);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public void RoomDoesNotExist()
    {
        // Act
        var exists = _repository.Exists(1, 100);

        // Assert
        Assert.False(exists);
    }
}