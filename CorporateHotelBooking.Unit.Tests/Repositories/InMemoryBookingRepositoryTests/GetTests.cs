using AutoFixture.Xunit2;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Unit.Tests.Helpers;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryBookingRepositoryTests;

public class GetTests
{
    private readonly InMemoryBookingRepository _repository;

    public GetTests()
    {
        _repository = new InMemoryBookingRepository();
    }

    [Theory, AutoData]
    public void BookingDoesNotExist(int bookingId)
    {
        // Act
        Action action = () => _repository.Get(bookingId);

        // Assert
        action.Should().Throw<BookingNotFoundException>();
    }

    [Fact]
    public void BookingExists()
    {
        // Arrange
        var booking = BookingFactory.CreateRandom();
        var createdBooking = _repository.Add(booking);

        // Act
        var result = _repository.Get(createdBooking.Id.Value);

        // Assert
        result.Id.Should().Be(createdBooking.Id.Value);
        result.Should().BeEquivalentTo(booking, options => options.Excluding(b => b.Id));
    }
}