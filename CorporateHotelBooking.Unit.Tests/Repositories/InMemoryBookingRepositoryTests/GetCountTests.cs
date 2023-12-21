using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Unit.Tests.Helpers;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryBookingRepositoryTests;

public class GetCountTests
{
    private readonly InMemoryBookingRepository _repository;

    public GetCountTests()
    {
        _repository = new InMemoryBookingRepository();
    }

    [Theory, AutoData]
    public void NoBookings(int hotelId, RoomType roomType)
    {
        // Act
        var count = _repository.GetCount(hotelId, roomType, DateOnly.MinValue, DateOnly.MaxValue);

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void OneBookingInTheSameDates()
    {
        // Arrange
        var booking = BookingFactory.CreateRandom();
        _repository.Add(booking);

        // Act
        var count = _repository.GetCount(booking.HotelId, booking.RoomType, booking.CheckInDate, booking.CheckOutDate);

        // Assert
        Assert.Equal(1, count);
    }

    [Fact]
    public void OneBookingInTheSameDatesButDifferentHotel()
    {
        // Arrange
        var booking = BookingFactory.CreateRandom();
        _repository.Add(booking);
        var anotherHotelId = booking.HotelId + 1;

        // Act
        var count = _repository.GetCount(anotherHotelId, booking.RoomType, booking.CheckInDate, booking.CheckOutDate);

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void OneBookingInTheSameDatesButDifferentRoomType()
    {
        // Arrange
        var booking = BookingFactory.CreateRandom();
        _repository.Add(booking);

        // Act
        var count = _repository.GetCount(
            booking.HotelId,
            RoomTypeProvider.NotContainedIn(booking.RoomType),
            booking.CheckInDate,
            booking.CheckOutDate);

        // Assert
        Assert.Equal(0, count);
    }

    [Theory, AutoData]
    public void OneBookingWithOnlyOverlappingFirstDay(int employeeId, int hotelId, RoomType roomType)
    {
        // Arrange
        _repository.Add(new Booking(employeeId, hotelId, roomType, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2)));

        // Act
        var count = _repository.GetCount(hotelId, roomType, new DateOnly(2021, 1, 2), new DateOnly(2021, 1, 3));

        // Assert
        Assert.Equal(1, count);
    }

    [Theory, AutoData]
    public void OneBookingWithOnlyOverlappingLastDay(int employeeId, int hotelId, RoomType roomType)
    {
        // Arrange
        _repository.Add(new Booking(employeeId, hotelId, roomType, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2)));

        // Act
        var count = _repository.GetCount(hotelId, roomType, new DateOnly(2020, 12, 31), new DateOnly(2021, 1, 1));

        // Assert
        Assert.Equal(1, count);
    }

    [Theory, AutoData]
    public void OneBookingWithNoOverlappingDates(int employeeId, int hotelId, RoomType roomType)
    {
        // Arrange
        _repository.Add(new Booking(employeeId, hotelId, roomType, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2)));

        // Act
        var count = _repository.GetCount(hotelId, roomType, new DateOnly(2021, 1, 3), new DateOnly(2021, 1, 4));

        // Assert
        Assert.Equal(0, count);
    }
}