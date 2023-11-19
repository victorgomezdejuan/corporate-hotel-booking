using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

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
        var existingRoom = new Room(1, 100, RoomType.Standard);
        _repository.AddRoom(existingRoom);
        var updatedRoom = new Room(1, 100, RoomType.JuniorSuite);

        // Act
        _repository.UpdateRoom(updatedRoom);

        // Assert
        var retrievedRoom = _repository.GetRoom(1, 100);
        Assert.Equal(updatedRoom, retrievedRoom);
    }
}