using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Unit.Tests.Helpers;
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Domain;

public class CompanyBookingPolicyTests
{
    [Theory, AutoData]
    public void BookingAllowed(int companyId, [CollectionSize(2)] List<RoomType> allowedRoomTypes)
    {
        // Arrange
        var companyBookingPolicy = new CompanyBookingPolicy(companyId, allowedRoomTypes);

        // Act
        var bookingAllowed = companyBookingPolicy.BookingAllowed(allowedRoomTypes[1]);

        // Assert
        bookingAllowed.Should().BeTrue();
    }

    [Theory, AutoData]
    public void BookingNotAllowed(int companyId, [CollectionSize(2)] List<RoomType> allowedRoomTypes)
    {
        // Arrange
        var companyBookingPolicy = new CompanyBookingPolicy(companyId, allowedRoomTypes);

        // Act
        var bookingAllowed = companyBookingPolicy.BookingAllowed(RoomTypeProvider.NotContainedIn(allowedRoomTypes));

        // Assert
        bookingAllowed.Should().BeFalse();
    }
}