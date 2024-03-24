using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class GetRoomCountTests
{
    private readonly InMemoryRoomRepository _repository;

    public GetRoomCountTests()
    {
        _repository = new InMemoryRoomRepository();
    }

    [Theory, AutoData]
    public void NoRooms(int hotelId, RoomType roomType)
    {
        // Act
        var count = _repository.GetCount(hotelId, roomType);

        // Assert
        count.Should().Be(0);
    }

    [Theory, AutoData]
    public void OneRoom(Room room)
    {
        // Arrange
        _repository.Add(room);

        // Act
        var count = _repository.GetCount(room.HotelId, room.Type);

        // Assert
        count.Should().Be(1);
    }

    [Theory, AutoData]
    public void MultipleRooms(int hotelId, RoomType roomType)
    {
        // Arrange
        _repository.Add(new Room(hotelId, 100, roomType));
        _repository.Add(new Room(hotelId, 101, roomType));
        _repository.Add(new Room(hotelId, 102, roomType));

        // Act
        var count = _repository.GetCount(hotelId, roomType);

        // Assert
        count.Should().Be(3);
    }
}