using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;
using CorporateHotelBooking.Unit.Tests.Helpers;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class UpdateTests
{
    [Theory, AutoData]
    public void UpdateExistingRoom(Room room)
    {
        // Arrange
        var repository = new InMemoryRoomRepository();
        repository.Add(room);
        var updatedRoom = new Room(room.HotelId, room.Number, RoomTypeProvider.NotContainedIn(room.Type));

        // Act
        repository.Update(updatedRoom);

        // Assert
        var retrievedRoom = repository.Get(room.HotelId, room.Number);
        Assert.Equal(updatedRoom, retrievedRoom);
    }
}