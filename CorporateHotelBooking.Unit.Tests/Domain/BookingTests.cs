using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Exceptions;
using CorporateHotelBooking.Unit.Tests.Helpers;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Domain;

public class BookingTests
{
    [Fact]
    public void CheckOutDateIsTheSameDayAsCheckInDate()
    {
        // Arrange
        var booking = BookingFactory.CreateRandom();

        // Act
        Action act = () => new Booking(
            booking.EmployeeId,
            booking.HotelId,
            booking.RoomType,
            DateUtils.Today(),
            DateUtils.Today());
        
        // Assert
        act.Should().Throw<CheckOutDateMustBeAfterCheckInDateException>();
    }

    [Fact]
    public void CheckOutDateIsBeforeCheckInDate()
    {
        // Arrange
        var booking = BookingFactory.CreateRandom();

        // Act
        Action act = () => new Booking(
            booking.EmployeeId,
            booking.HotelId,
            booking.RoomType,
            DateUtils.Today(),
            DateUtils.Today().AddDays(-1));

        // Assert
        act.Should().Throw<CheckOutDateMustBeAfterCheckInDateException>();
    }

    [Fact]
    public void CheckOutDateIsAfterCheckInDate()
    {
        // Arrange
        var booking = BookingFactory.CreateRandom();

        // Act
        var createdBooking = new Booking(
            booking.EmployeeId,
            booking.HotelId,
            booking.RoomType,
            DateUtils.Today(),
            DateUtils.Today().AddDays(1));

        // Assert
        createdBooking.CheckInDate.Should().Be(DateUtils.Today());
        createdBooking.CheckOutDate.Should().Be(DateUtils.Today().AddDays(1));
    }
}