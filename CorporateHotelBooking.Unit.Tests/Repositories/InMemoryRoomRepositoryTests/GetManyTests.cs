using FluentAssertions;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;
using AutoFixture.Xunit2;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class GetManyTests
{
    private readonly InMemoryRoomRepository _repository;

    public GetManyTests()
    {
        _repository = new InMemoryRoomRepository();
    }

    [Theory, AutoData]
    public void GetNoRooms(int hotelId)
    {
        // Act
        var rooms = _repository.GetMany(hotelId);

        // Assert
        rooms.Should().BeEmpty();
    }

    [Theory, AutoData]
    public void GetOneRoom(Room room)
    {
        // Arrange
        _repository.Add(room);

        // Act
        var rooms = _repository.GetMany(room.HotelId);

        // Assert
        rooms.Should().HaveCount(1);
        rooms.First().Should().Be(room);
    }

    [Theory, AutoData]
    public void GetMultipleRooms(int hotelId, int roomNumber1, int roomNumber2, RoomType roomType1, RoomType roomType2)
    {
        // Arrange
        _repository.Add(new Room(hotelId, roomNumber1, roomType1));
        _repository.Add(new Room(hotelId, roomNumber2, roomType2));

        // Act
        var rooms = _repository.GetMany(hotelId);

        // Assert
        rooms.Should().HaveCount(2);
        rooms.First().Number.Should().Be(roomNumber1);
        rooms.First().Type.Should().Be(roomType1);
        rooms.Last().Number.Should().Be(roomNumber2);
        rooms.Last().Type.Should().Be(roomType2);
    }
}