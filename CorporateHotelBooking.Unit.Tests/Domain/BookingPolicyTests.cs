using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Domain;

public class BookingPolicyTests
{
    [Theory, AutoData]
    public void CheckBookingAllowanceForANonApplicableBookingPolicy(RoomType roomType)
    {
        // Arrange
        var NonApplicableBookingPolicy = new NonApplicableBookingPolicy();

        // Act
        Action act = () => NonApplicableBookingPolicy.BookingAllowed(roomType);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Theory, AutoData]
    public void CheckBookingAllowanceForAnEmployeeBookingPolicy(int employeeId, [CollectionSize(2)] List<RoomType> allowedRoomTypes)
    {
        // Arrange
        var employeeBookingPolicy = new EmployeeBookingPolicy(employeeId, allowedRoomTypes);

        // Act
        var bookingAllowed = employeeBookingPolicy.BookingAllowed(allowedRoomTypes[0]);

        // Assert
        bookingAllowed.Should().BeTrue();
    }

    [Theory, AutoData]
    public void CheckBookingAllowanceForACompanyBookingPolicy(int companyId, [CollectionSize(2)] List<RoomType> allowedRoomTypes)
    {
        // Arrange
        var companyBookingPolicy = new CompanyBookingPolicy(companyId, allowedRoomTypes);

        // Act
        var bookingAllowed = companyBookingPolicy.BookingAllowed(allowedRoomTypes[0]);

        // Assert
        bookingAllowed.Should().BeTrue();
    }
}