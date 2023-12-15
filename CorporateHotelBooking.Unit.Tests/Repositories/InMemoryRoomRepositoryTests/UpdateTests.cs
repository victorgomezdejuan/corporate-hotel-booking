using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class UpdateTests
{
    [Fact]
    public void UpdateExistingRoom()
    {
        // Arrange
        var repository = new InMemoryRoomRepository();
        var existingRoom = new Room(1, 100, RoomType.Standard);
        repository.Add(existingRoom);
        var updatedRoom = new Room(1, 100, RoomType.JuniorSuite);

        // Act
        repository.Update(updatedRoom);

        // Assert
        var retrievedRoom = repository.Get(1, 100);
        Assert.Equal(updatedRoom, retrievedRoom);
    }
}