using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class GetTests
{
    [Theory, AutoData]
    public void GetAnExistingRoom(Room room)
    {
        // Arrange
        var repository = new InMemoryRoomRepository(); 
        repository.Add(room);

        // Act
        var retrievedRoom = repository.Get(room.HotelId, room.Number);

        // Assert
        retrievedRoom.Should().Be(room);
    }
}