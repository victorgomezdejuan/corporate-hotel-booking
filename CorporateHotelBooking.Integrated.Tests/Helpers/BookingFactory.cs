using AutoFixture;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.ValueObjects;
using CorporateHotelBooking.Integrated.Tests.Helpers.AutoFixture;

namespace CorporateHotelBooking.Integrated.Tests.Helpers;

public class BookingFactory
{
    public static Booking CreateRandom()
    {
        var fixture = new Fixture().Customize(new DateOnlyFixtureCustomization());
        var checkInDate = fixture.Create<DateOnly>();

        return new Booking(
            fixture.Create<int>(),
            fixture.Create<int>(),
            RoomType.Standard,
            new BookingDateRange(
                checkInDate,
                checkInDate.AddDays(fixture.Create<int>())));
    }
}