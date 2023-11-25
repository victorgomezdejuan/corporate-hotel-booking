using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Domain.Exceptions;
using CorporateHotelBooking.Unit.Tests.Helpers;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Domain;

public class BookingTests
{
    [Fact]
    public void CheckOutDateIsTheSameDayAsCheckInDate()
    {
        Action act = () => new Booking(1, 2, 3, RoomType.Standard, DateUtils.Today(), DateUtils.Today());
        act.Should().Throw<CheckOutDateMustBeAfterCheckInDateException>();
    }

    [Fact]
    public void CheckOutDateIsBeforeCheckInDate()
    {
        Action act = () => new Booking(1, 2, 3, RoomType.Standard, DateUtils.Today(), DateUtils.Today().AddDays(-1));
        act.Should().Throw<CheckOutDateMustBeAfterCheckInDateException>();
    }

    [Fact]
    public void CheckOutDateIsAfterCheckInDate()
    {
        var booking = new Booking(1, 2, 3, RoomType.Standard, DateUtils.Today(), DateUtils.Today().AddDays(1));
        booking.CheckInDate.Should().Be(DateUtils.Today());
        booking.CheckOutDate.Should().Be(DateUtils.Today().AddDays(1));
    }
}