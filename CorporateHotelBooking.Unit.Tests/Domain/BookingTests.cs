using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Domain.Exceptions;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Domain;

public class BookingTests
{
    [Fact]
    public void CheckOutDateIsTheSameDayAsCheckInDate()
    {
        Action act = () => new Booking(1, 2, 3, RoomType.Standard, Today(), Today());
        act.Should().Throw<CheckOutDateMustBeAfterCheckInDateException>();
    }

    [Fact]
    public void CheckOutDateIsBeforeCheckInDate()
    {
        Action act = () => new Booking(1, 2, 3, RoomType.Standard, Today(), Today().AddDays(-1));
        act.Should().Throw<CheckOutDateMustBeAfterCheckInDateException>();
    }

    [Fact]
    public void CheckOutDateIsAfterCheckInDate()
    {
        var booking = new Booking(1, 2, 3, RoomType.Standard, Today(), Today().AddDays(1));
        booking.CheckInDate.Should().Be(Today());
        booking.CheckOutDate.Should().Be(Today().AddDays(1));
    }

    private static DateOnly Today()
    {
        return DateOnly.FromDateTime(DateTime.Now);
    }
}