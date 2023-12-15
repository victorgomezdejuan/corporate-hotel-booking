using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class GetTests
{
    [Fact]
    public void GetAnExistingRoom()
    {
        // Arrange
        var repository = new InMemoryRoomRepository(); 
        var existingRoom = new Room(1, 100, RoomType.Standard);
        repository.Add(existingRoom);

        // Act
        var retrievedRoom = repository.Get(1, 100);

        // Assert
        Assert.Equal(existingRoom, retrievedRoom);
    }
}