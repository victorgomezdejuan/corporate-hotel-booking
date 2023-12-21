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
        var booking = BookingFactory.CreateRandom();
        var repository = new InMemoryBookingRepository();

        // Act
        Booking result = repository.Add(booking);

        // Assert
        result.Id.Should().NotBeNull();
        result.Should().BeEquivalentTo(booking, options => options.Excluding(b => b.Id));
        repository.Get(result.Id.Value).Should().BeEquivalentTo(booking, options => options.Excluding(b => b.Id));
    }
}