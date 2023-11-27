using CorporateHotelBooking.Domain;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Domain;

public class BookingPolicyTests
{
    [Fact]
    public void CheckBookingAllowanceForANonApplicableBookingPolicy()
    {
        // Arrange
        var NonApplicableBookingPolicy = new NonApplicableBookingPolicy();

        // Act
        Action act = () => NonApplicableBookingPolicy.BookingAllowed(RoomType.Standard);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void CheckBookingAllowanceForAnEmployeeBookingPolicy()
    {
        // Arrange
        var employeeBookingPolicy = new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        var bookingAllowed = employeeBookingPolicy.BookingAllowed(RoomType.Standard);

        // Assert
        bookingAllowed.Should().BeTrue();
    }

    [Fact]
    public void CheckBookingAllowanceForACompanyBookingPolicy()
    {
        // Arrange
        var companyBookingPolicy = new CompanyBookingPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        var bookingAllowed = companyBookingPolicy.BookingAllowed(RoomType.Standard);

        // Assert
        bookingAllowed.Should().BeTrue();
    }
}