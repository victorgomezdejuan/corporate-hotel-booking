using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Unit.Tests.Helpers;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryBookingRepositoryTests;

public class DeleteByEmployeeTests
{
    [Theory, AutoData]
    public void DeleteSeveralBookings(int employeeId, RoomType roomType)
    {
        // Arrange
        var bookingRepository = new InMemoryBookingRepository();
        var booking1 = bookingRepository.Add(BookingFactory.CreateRandomWithEmployeeAndRoomType(employeeId, roomType));
        var booking2 = bookingRepository.Add(BookingFactory.CreateRandomWithEmployeeAndRoomType(employeeId, roomType));
        var booking3 = bookingRepository.Add(BookingFactory.CreateRandomWithEmployeeAndRoomType(employeeId, roomType));

        // Act
        bookingRepository.DeleteByEmployee(employeeId);

        // Assert
        bookingRepository.Get(booking1.Id.Value).Should().BeNull();
        bookingRepository.Get(booking2.Id.Value).Should().BeNull();
        bookingRepository.Get(booking3.Id.Value).Should().BeNull();
    }

    [Theory, AutoData]
    public void DeleteNoBookings(int employeeId, RoomType roomType)
    {
        // Arrange
        var bookingRepository = new InMemoryBookingRepository();
        var booking1 = bookingRepository.Add(BookingFactory.CreateRandomWithEmployeeAndRoomType(employeeId, roomType));
        var booking2 = bookingRepository.Add(BookingFactory.CreateRandomWithEmployeeAndRoomType(employeeId, roomType));
        var booking3 = bookingRepository.Add(BookingFactory.CreateRandomWithEmployeeAndRoomType(employeeId, roomType));
        var anotherEmployeeId = employeeId + 1;

        // Act
        bookingRepository.DeleteByEmployee(anotherEmployeeId);

        // Assert
        bookingRepository.Get(booking1.Id.Value).Should().NotBeNull();
        bookingRepository.Get(booking2.Id.Value).Should().NotBeNull();
        bookingRepository.Get(booking3.Id.Value).Should().NotBeNull();
    }
}