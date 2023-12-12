using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Unit.Tests.Helpers;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryBookingRepositoryTests;

public class AddTests
{
    [Fact]
    public void AddABooking()
    {
        // Arrange
        var booking = new Booking
        (
            employeeId: 1,
            hotelId: 1,
            roomType: RoomType.Standard,
            checkInDate: DateUtils.Today(),
            checkOutDate: DateUtils.Today().AddDays(1)
        );
        var repository = new InMemoryBookingRepository();

        // Act
        Booking result = repository.Add(booking);

        // Assert
        var expectedBooking = new Booking
        (
            id: 1,
            employeeId: 1,
            hotelId: 1,
            roomType: RoomType.Standard,
            checkInDate: DateUtils.Today(),
            checkOutDate: DateUtils.Today().AddDays(1)
        );
        result.Should().BeEquivalentTo(expectedBooking);
        repository.Get(result.Id.Value).Should().Be(expectedBooking);
    }
}