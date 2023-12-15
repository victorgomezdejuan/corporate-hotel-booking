using FluentAssertions;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryRoomRepositoryTests;

public class GetManyTests
{
    private readonly InMemoryRoomRepository _repository;

    public GetManyTests()
    {
        _repository = new InMemoryRoomRepository();
    }

    [Fact]
    public void GetNoRooms()
    {
        // Act
        var rooms = _repository.GetMany(1);

        // Assert
        rooms.Should().BeEmpty();
    }

    [Fact]
    public void GetOneRoom()
    {
        // Arrange
        _repository.Add(new Room(1, 100, RoomType.Standard));

        // Act
        var rooms = _repository.GetMany(1);

        // Assert
        rooms.Should().HaveCount(1);
        rooms.First().Number.Should().Be(100);
        rooms.First().Type.Should().Be(RoomType.Standard);
    }

    [Fact]
    public void GetMultipleRooms()
    {
        // Arrange
        _repository.Add(new Room(1, 100, RoomType.Standard));
        _repository.Add(new Room(1, 101, RoomType.JuniorSuite));

        // Act
        var rooms = _repository.GetMany(1);

        // Assert
        rooms.Should().HaveCount(2);
        rooms.First().Number.Should().Be(100);
        rooms.First().Type.Should().Be(RoomType.Standard);
        rooms.Last().Number.Should().Be(101);
        rooms.Last().Type.Should().Be(RoomType.JuniorSuite);
    }
}