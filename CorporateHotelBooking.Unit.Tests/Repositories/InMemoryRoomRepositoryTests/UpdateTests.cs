using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class UpdateTests
{
    private readonly InMemoryRoomRepository _repository;

    public UpdateTests()
    {
        _repository = new InMemoryRoomRepository();
    }

    [Fact]
    public void UpdateExistingRoom()
    {
        // Arrange
        var existingRoom = new Room(1, 100, RoomType.Standard);
        _repository.Add(existingRoom);
        var updatedRoom = new Room(1, 100, RoomType.JuniorSuite);

        // Act
        _repository.Update(updatedRoom);

        // Assert
        var retrievedRoom = _repository.Get(1, 100);
        Assert.Equal(updatedRoom, retrievedRoom);
    }
}