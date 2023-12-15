using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class AddTests
{
    [Fact]
    public void AddNewRoom()
    {
        // Arrange
        var repository = new InMemoryRoomRepository();
        var roomToBeAdded = new Room(1, 100, RoomType.Standard);

        // Act
        repository.Add(roomToBeAdded);

        // Assert
        var retrievedRoom = repository.Get(1, 100);
        Assert.Equal(roomToBeAdded, retrievedRoom);
    }
}