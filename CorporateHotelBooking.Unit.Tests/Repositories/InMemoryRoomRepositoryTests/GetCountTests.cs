using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class GetRoomCountTests
{
    private readonly InMemoryRoomRepository _repository;

    public GetRoomCountTests()
    {
        _repository = new InMemoryRoomRepository();
    }

    [Fact]
    public void NoRooms()
    {
        // Act
        var count = _repository.GetCount(1, RoomType.Standard);

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void OneRoom()
    {
        // Arrange
        _repository.Add(new Room(1, 100, RoomType.Standard));

        // Act
        var count = _repository.GetCount(1, RoomType.Standard);

        // Assert
        Assert.Equal(1, count);
    }

    [Fact]
    public void MultipleRooms()
    {
        // Arrange
        _repository.Add(new Room(1, 100, RoomType.Standard));
        _repository.Add(new Room(1, 101, RoomType.Standard));
        _repository.Add(new Room(1, 102, RoomType.Standard));

        // Act
        var count = _repository.GetCount(1, RoomType.Standard);

        // Assert
        Assert.Equal(3, count);
    }
}