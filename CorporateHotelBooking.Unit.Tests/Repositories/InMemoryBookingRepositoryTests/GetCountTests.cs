using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.ValueObjects;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Unit.Tests.Helpers;
using FluentAssertions;

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
        var count = _repository.GetCount(hotelId, roomType, new BookingDateRange(DateOnly.MinValue, DateOnly.MaxValue));

        // Assert
        count.Should().Be(0);
    }

    [Fact]
    public void OneBookingInTheSameDates()
    {
        // Arrange
        var booking = BookingFactory.CreateRandom();
        _repository.Add(booking);

        // Act
        var count = _repository.GetCount(booking.HotelId, booking.RoomType, booking.DateRange);

        // Assert
        count.Should().Be(1);
    }

    [Fact]
    public void OneBookingInTheSameDatesButDifferentHotel()
    {
        // Arrange
        var booking = BookingFactory.CreateRandom();
        _repository.Add(booking);
        var anotherHotelId = booking.HotelId + 1;

        // Act
        var count = _repository.GetCount(anotherHotelId, booking.RoomType, booking.DateRange);

        // Assert
        count.Should().Be(0);
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
            RoomTypeProvider.DifferentFrom(booking.RoomType),
            booking.DateRange);

        // Assert
        count.Should().Be(0);
    }

    [Theory, AutoData]
    public void OneBookingWithOnlyOverlappingFirstDay(int employeeId, int hotelId, RoomType roomType)
    {
        // Arrange
        _repository.Add(new Booking(
            employeeId,
            hotelId,
            roomType,
            new BookingDateRange(new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2))));

        // Act
        var count = _repository.GetCount(
            hotelId,
            roomType,
            new BookingDateRange(new DateOnly(2021, 1, 2), new DateOnly(2021, 1, 3)));

        // Assert
        count.Should().Be(1);
    }

    [Theory, AutoData]
    public void OneBookingWithOnlyOverlappingLastDay(int employeeId, int hotelId, RoomType roomType)
    {
        // Arrange
        _repository.Add(new Booking(
            employeeId,
            hotelId,
            roomType,
            new BookingDateRange(new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2))));

        // Act
        var count = _repository.GetCount(
            hotelId,
            roomType,
            new BookingDateRange(new DateOnly(2020, 12, 31), new DateOnly(2021, 1, 1)));

        // Assert
        count.Should().Be(1);
    }

    [Theory, AutoData]
    public void OneBookingWithNoOverlappingDates(int employeeId, int hotelId, RoomType roomType)
    {
        // Arrange
        _repository.Add(new Booking(
            employeeId,
            hotelId,
            roomType,
            new BookingDateRange(new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2))));

        // Act
        var count = _repository.GetCount(
            hotelId,
            roomType,
            new BookingDateRange(new DateOnly(2021, 1, 3), new DateOnly(2021, 1, 4)));

        // Assert
        count.Should().Be(0);
    }
}