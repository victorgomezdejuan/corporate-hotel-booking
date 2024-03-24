using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Domain;

public class AggregatedBookingPolicyTests
{
    [Theory, AutoData]
    public void BookingAllowedByCompanyBookingPolicy(int companyId, [CollectionSize(2)] List<RoomType> allowedRoomTypes)
    {
        // Arrange
        var employeeBookingPolicy = new NonApplicableBookingPolicy();
        var companyBookingPolicy = new CompanyBookingPolicy(companyId, allowedRoomTypes);
        var bookingPolicy = new AggregatedBookingPolicy(employeeBookingPolicy, companyBookingPolicy);

        // Act
        var bookingAllowed = bookingPolicy.BookingAllowed(allowedRoomTypes[0]);

        // Assert
        bookingAllowed.Should().BeTrue();
    }

    [Theory, AutoData]
    public void BookingAllowedByEmployeeBookingPolicy(
        int employeeId,
        [CollectionSize(2)] List<RoomType> allowedRoomTypes)
    {
        // Arrange
        var employeeBookingPolicy = new EmployeeBookingPolicy(
            employeeId,
            allowedRoomTypes);
        var companyBookingPolicy = new NonApplicableBookingPolicy();
        var bookingPolicy = new AggregatedBookingPolicy(employeeBookingPolicy, companyBookingPolicy);

        // Act
        var bookingAllowed = bookingPolicy.BookingAllowed(allowedRoomTypes[1]);

        // Assert
        bookingAllowed.Should().BeTrue();
    }

    [Theory, AutoData]
    public void BookingAllowedByEmployeeBookingPolicyButNotByCompanyBookingPolicy(int employeeId, int companyId)
    {
        // Arrange
        var employeeBookingPolicy = new EmployeeBookingPolicy(
            employeeId,
            new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });
        var companyBookingPolicy = new CompanyBookingPolicy(
            companyId,
            new List<RoomType> { RoomType.JuniorSuite });
        var bookingPolicy = new AggregatedBookingPolicy(employeeBookingPolicy, companyBookingPolicy);

        // Act
        var bookingAllowed = bookingPolicy.BookingAllowed(RoomType.Standard);

        // Assert
        bookingAllowed.Should().BeTrue();
    }

    [Theory, AutoData]
    public void BookingAllowedByCompanyBookingPolicyButNotByEmployeeBookingPolicy(int employeeId, int companyId)
    {
        // Arrange
        var employeeBookingPolicy = new EmployeeBookingPolicy(
            employeeId,
            new List<RoomType> { RoomType.JuniorSuite });
        var companyBookingPolicy = new CompanyBookingPolicy(
            companyId,
            new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });
        var bookingPolicy = new AggregatedBookingPolicy(employeeBookingPolicy, companyBookingPolicy);

        // Act
        var bookingAllowed = bookingPolicy.BookingAllowed(RoomType.Standard);

        // Assert
        bookingAllowed.Should().BeFalse();
    }

    [Theory, AutoData]
    public void BookingAllowedByNeitherCompanyNorEmployeeBookingPolicy(int employeeId, int companyId)
    {
        // Arrange
        var employeeBookingPolicy = new EmployeeBookingPolicy(employeeId, new List<RoomType> { RoomType.JuniorSuite });
        var companyBookingPolicy = new CompanyBookingPolicy(companyId, new List<RoomType> { RoomType.JuniorSuite });
        var bookingPolicy = new AggregatedBookingPolicy(employeeBookingPolicy, companyBookingPolicy);

        // Act
        var bookingAllowed = bookingPolicy.BookingAllowed(RoomType.Standard);

        // Assert
        bookingAllowed.Should().BeFalse();
    }

    [Theory, AutoData]
    public void NoBookingPoliciesForAnEmployee(RoomType roomType)
    {
        // Arrange
        var employeeBookingPolicy = new NonApplicableBookingPolicy();
        var companyBookingPolicy = new NonApplicableBookingPolicy();
        var bookingPolicy = new AggregatedBookingPolicy(employeeBookingPolicy, companyBookingPolicy);

        // Act
        var bookingAllowed = bookingPolicy.BookingAllowed(roomType);

        // Assert
        bookingAllowed.Should().BeTrue();
    }
}