using CorporateHotelBooking.Domain;
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
    public void RoomExists()
    {
        // Arrange
        _repository.AddRoom(new Room(1, 100, RoomType.Standard));

        // Act
        var exists = _repository.ExistsRoomType(1, 100);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public void RoomDoesNotExist()
    {
        // Act
        var exists = _repository.ExistsRoomType(1, 100);

        // Assert
        Assert.False(exists);
    }
}