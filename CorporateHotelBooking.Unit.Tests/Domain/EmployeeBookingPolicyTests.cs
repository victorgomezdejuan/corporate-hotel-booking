using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Unit.Tests.Helpers;
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Domain;

public class EmployeeBookingPolicyTests
{
    [Theory, AutoData]
    public void BookingAllowed(int employeeId, [CollectionSize(2)] List<RoomType> allowedRoomTypes)
    {
        // Arrange
        var employeeBookingPolicy = new EmployeeBookingPolicy(employeeId, allowedRoomTypes);

        // Act
        var bookingAllowed = employeeBookingPolicy.BookingAllowed(allowedRoomTypes[1]);

        // Assert
        bookingAllowed.Should().BeTrue();
    }

    [Theory, AutoData]
    public void BookingNotAllowed(int employeeId, [CollectionSize(2)] List<RoomType> allowedRoomTypes)
    {
        // Arrange
        var employeeBookingPolicy = new EmployeeBookingPolicy(employeeId, allowedRoomTypes);

        // Act
        var bookingAllowed = employeeBookingPolicy.BookingAllowed(RoomTypeProvider.NotContainedIn(allowedRoomTypes));

        // Assert
        bookingAllowed.Should().BeFalse();
    }
}