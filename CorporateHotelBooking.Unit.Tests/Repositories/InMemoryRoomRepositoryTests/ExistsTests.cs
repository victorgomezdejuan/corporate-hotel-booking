using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class ExistsTests
{
    private readonly InMemoryRoomRepository _repository;

    public ExistsTests()
    {
        _repository = new InMemoryRoomRepository();
    }

    [Theory, AutoData]
    public void RoomNumberExists(Room room)
    {
        // Arrange
        _repository.Add(room);

        // Act
        var exists = _repository.ExistsRoomNumber(room.HotelId, room.Number);

        // Assert
        exists.Should().BeTrue();
    }

    [Theory, AutoData]
    public void RoomNumberDoesNotExist(int hotelId, int roomNumber)
    {
        // Act
        var exists = _repository.ExistsRoomNumber(hotelId, roomNumber);

        // Assert
        exists.Should().BeFalse();
    }

    [Theory, AutoData]
    public void RoomTypeExists(Room room)
    {
        // Arrange
        _repository.Add(room);

        // Act
        var exists = _repository.ExistsRoomType(room.HotelId, room.Type);

        // Assert
        exists.Should().BeTrue();
    }

    [Theory, AutoData]
    public void RoomTypeDoesNotExist(int hotelId, RoomType roomType)
    {
        // Act
        var exists = _repository.ExistsRoomType(hotelId, roomType);

        // Assert
        exists.Should().BeFalse();
    }
}