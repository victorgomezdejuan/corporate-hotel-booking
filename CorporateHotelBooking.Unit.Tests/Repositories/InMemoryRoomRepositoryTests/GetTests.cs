using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class GetTests
{
    private readonly InMemoryRoomRepository _repository;

    public GetTests()
    {
        _repository = new InMemoryRoomRepository();
    }

    [Fact]
    public void GetRoom()
    {
        // Arrange
        var existingRoom = new Room(1, 100, RoomType.Standard);
        _repository.Add(existingRoom);

        // Act
        var retrievedRoom = _repository.Get(1, 100);

        // Assert
        Assert.Equal(existingRoom, retrievedRoom);
    }
}