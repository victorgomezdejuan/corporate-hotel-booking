using AutoFixture;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;

namespace CorporateHotelBooking.Unit.Tests.Helpers;

public class BookingFactory
{
    internal static Booking CreateRandom()
    {
        var fixture = new Fixture().Customize(new DateOnlyFixtureCustomization());
        var checkInDate = fixture.Create<DateOnly>();

        return new Booking(
            fixture.Create<int>(),
            fixture.Create<int>(),
            RoomType.Standard,
            checkInDate,
            checkInDate.AddDays(fixture.Create<int>()));
    }

    internal static Booking CreateRandomWithEmployeeAndRoomType(int employeeId, RoomType roomType)
    {
        var fixture = new Fixture().Customize(new DateOnlyFixtureCustomization());
        var checkInDate = fixture.Create<DateOnly>();

        return new Booking(
            employeeId,
            fixture.Create<int>(),
            roomType,
            checkInDate,
            checkInDate.AddDays(fixture.Create<int>()));
    }
}