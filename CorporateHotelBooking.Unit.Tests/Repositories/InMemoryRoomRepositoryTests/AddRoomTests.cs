using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class AddRoomTests
{
    private readonly InMemoryRoomRepository _repository;

    public AddRoomTests()
    {
        _repository = new InMemoryRoomRepository();
    }

    [Fact]
    public void AddNewRoom()
    {
        // Arrange
        var roomToBeAdded = new Room(1, 100, RoomType.Standard);

        // Act
        _repository.AddRoom(roomToBeAdded);

        // Assert
        var retrievedRoom = _repository.GetRoom(1, 100);
        Assert.Equal(roomToBeAdded, retrievedRoom);
    }
}