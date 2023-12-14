using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Bookings;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryBookingRepositoryTests;

public class DeleteByEmployeeTests
{
    [Fact]
    public void DeleteSeveralBookings()
    {
        // Arrange
        var bookingRepository = new InMemoryBookingRepository();
        bookingRepository.Add(new Booking(1,1, 1,RoomType.Standard,new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2)));
        bookingRepository.Add(new Booking(2,1, 1,RoomType.Standard,new DateOnly(2021, 1, 3), new DateOnly(2021, 1, 4)));
        bookingRepository.Add(new Booking(3,1, 1,RoomType.Standard,new DateOnly(2021, 1, 5), new DateOnly(2021, 1, 6)));

        // Act
        bookingRepository.DeleteByEmployee(1);

        // Assert
        bookingRepository.GetCount(1, RoomType.Standard, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 6)).Should().Be(0);
    }
}