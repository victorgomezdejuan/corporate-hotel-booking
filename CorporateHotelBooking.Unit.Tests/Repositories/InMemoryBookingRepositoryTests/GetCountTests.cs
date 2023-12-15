using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Bookings;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryBookingRepositoryTests;

public class GetCountTests
{
    private readonly InMemoryBookingRepository _repository;

    public GetCountTests()
    {
        _repository = new InMemoryBookingRepository();
    }

    [Fact]
    public void NoBookings()
    {
        // Act
        var count = _repository.GetCount(1, RoomType.Standard, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2));

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void OneBookingInTheSameDates()
    {
        // Arrange
        _repository.Add(new Booking(10, 1, RoomType.Standard, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2)));

        // Act
        var count = _repository.GetCount(1, RoomType.Standard, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2));

        // Assert
        Assert.Equal(1, count);
    }

    [Fact]
    public void OneBookingInTheSameDatesButDifferentHotel()
    {
        // Arrange
        _repository.Add(new Booking(10, 1, RoomType.Standard, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2)));

        // Act
        var count = _repository.GetCount(2, RoomType.Standard, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2));

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void OneBookingInTheSameDatesButDifferentRoomType()
    {
        // Arrange
        _repository.Add(new Booking(10, 1, RoomType.Standard, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2)));

        // Act
        var count = _repository.GetCount(1, RoomType.JuniorSuite, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2));

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void OneBookingWithOnlyOverlappingFirstDay()
    {
        // Arrange
        _repository.Add(new Booking(10, 1, RoomType.Standard, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2)));

        // Act
        var count = _repository.GetCount(1, RoomType.Standard, new DateOnly(2021, 1, 2), new DateOnly(2021, 1, 3));

        // Assert
        Assert.Equal(1, count);
    }

    [Fact]
    public void OneBookingWithOnlyOverlappingLastDay()
    {
        // Arrange
        _repository.Add(new Booking(10, 1, RoomType.Standard, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2)));

        // Act
        var count = _repository.GetCount(1, RoomType.Standard, new DateOnly(2020, 12, 31), new DateOnly(2021, 1, 1));

        // Assert
        Assert.Equal(1, count);
    }

    [Fact]
    public void OneBookingWithNoOverlappingDates()
    {
        // Arrange
        _repository.Add(new Booking(10, 1, RoomType.Standard, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2)));

        // Act
        var count = _repository.GetCount(1, RoomType.Standard, new DateOnly(2021, 1, 3), new DateOnly(2021, 1, 4));

        // Assert
        Assert.Equal(0, count);
    }
}