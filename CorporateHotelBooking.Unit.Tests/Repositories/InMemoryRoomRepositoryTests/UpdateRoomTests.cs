using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class UpdateRoomTests
{
    private readonly InMemoryRoomRepository _repository;

    public UpdateRoomTests()
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