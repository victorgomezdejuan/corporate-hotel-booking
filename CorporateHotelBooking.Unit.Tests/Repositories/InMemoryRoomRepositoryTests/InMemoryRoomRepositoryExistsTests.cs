using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class InMemoryRoomRepositoryExistsTests
{
    private readonly InMemoryRoomRepository _repository;

    public InMemoryRoomRepositoryExistsTests()
    {
        _repository = new InMemoryRoomRepository();
    }

    [Fact]
    public void RoomNumberExists()
    {
        // Arrange
        _repository.AddRoom(new Room(1, 100, RoomType.Standard));

        // Act
        var exists = _repository.ExistsRoomType(1, 100);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public void RoomNumberDoesNotExist()
    {
        // Act
        var exists = _repository.ExistsRoomType(1, 100);

        // Assert
        Assert.False(exists);
    }

    [Fact]
    public void RoomTypeExists()
    {
        // Arrange
        _repository.AddRoom(new Room(1, 100, RoomType.Standard));

        // Act
        var exists = _repository.ExistsRoomType(1, RoomType.Standard);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public void RoomTypeDoesNotExist()
    {
        // Act
        var exists = _repository.ExistsRoomType(1, RoomType.Standard);

        // Assert
        Assert.False(exists);
    }
}