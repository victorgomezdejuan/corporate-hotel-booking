using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Bookings;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryBookingRepositoryTests;

public class GetTests
{
    private readonly InMemoryBookingRepository _repository;

    public GetTests()
    {
        _repository = new InMemoryBookingRepository();
    }

    [Fact]
    public void BookingDoesNotExist()
    {
        // Act
        Action action = () => _repository.Get(1);

        // Assert
        action.Should().Throw<BookingNotFoundException>();
    }

    [Fact]
    public void BookingExists()
    {
        // Arrange
        var booking = new Booking(1, 10, 100, RoomType.Standard, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2));
        _repository.Add(booking);

        // Act
        var result = _repository.Get(1);

        // Assert
        result.Should().Be(booking);
    }
}