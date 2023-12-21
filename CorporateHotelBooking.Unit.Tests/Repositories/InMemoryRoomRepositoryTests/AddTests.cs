using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class AddTests
{
    [Theory, AutoData]
    public void AddNewRoom(Room room)
    {
        // Arrange
        var repository = new InMemoryRoomRepository();

        // Act
        repository.Add(room);

        // Assert
        var retrievedRoom = repository.Get(room.HotelId, room.Number);
        Assert.Equal(room, retrievedRoom);
    }
}