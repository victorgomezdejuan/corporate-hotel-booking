using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Bookings;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryBookingRepositoryTests;

public class GetTests
{
    [Fact]
    public void BookingDoesNotExist()
    {
        // Arrange
        var repository = new InMemoryBookingRepository();

        // Act
        Action action = () => repository.Get(1);

        // Assert
        action.Should().Throw<BookingNotFoundException>();
    }

    [Fact]
    public void BookingExists()
    {
        // Arrange
        var repository = new InMemoryBookingRepository();
        var booking = new Booking(1, 10, 100, RoomType.Standard, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2));
        repository.Add(booking);

        // Act
        var result = repository.Get(1);

        // Assert
        result.Should().Be(booking);
    }
}