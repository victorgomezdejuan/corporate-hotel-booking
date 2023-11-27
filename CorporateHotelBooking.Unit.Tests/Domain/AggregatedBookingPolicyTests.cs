using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Unit.Tests.Domain;

public class AggregatedBookingPolicyTests
{
    [Fact]
    public void BookingAllowedByCompanyBookingPolicy()
    {
        // Arrange
        var employeeBookingPolicy = new NonApplicableBookingPolicy();
        var companyBookingPolicy = new CompanyBookingPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });
        var bookingPolicy = new AggregatedBookingPolicy(employeeBookingPolicy, companyBookingPolicy);

        // Act
        var bookingAllowed = bookingPolicy.BookingAllowed(RoomType.Standard);

        // Assert
        Assert.True(bookingAllowed);
    }

    [Fact]
    public void BookingAllowedByEmployeeBookingPolicy()
    {
        // Arrange
        var employeeBookingPolicy = new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });
        var companyBookingPolicy = new NonApplicableBookingPolicy();
        var bookingPolicy = new AggregatedBookingPolicy(employeeBookingPolicy, companyBookingPolicy);

        // Act
        var bookingAllowed = bookingPolicy.BookingAllowed(RoomType.Standard);

        // Assert
        Assert.True(bookingAllowed);
    }

    [Fact]
    public void BookingAllowedByEmployeeBookingPolicyButNotByCompanyBookingPolicy()
    {
        // Arrange
        var employeeBookingPolicy = new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });
        var companyBookingPolicy = new CompanyBookingPolicy(100, new List<RoomType> { RoomType.JuniorSuite });
        var bookingPolicy = new AggregatedBookingPolicy(employeeBookingPolicy, companyBookingPolicy);

        // Act
        var bookingAllowed = bookingPolicy.BookingAllowed(RoomType.Standard);

        // Assert
        Assert.True(bookingAllowed);
    }

    [Fact]
    public void BookingAllowedByCompanyBookingPolicyButNotByEmployeeBookingPolicy()
    {
        // Arrange
        var employeeBookingPolicy = new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.JuniorSuite });
        var companyBookingPolicy = new CompanyBookingPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });
        var bookingPolicy = new AggregatedBookingPolicy(employeeBookingPolicy, companyBookingPolicy);

        // Act
        var bookingAllowed = bookingPolicy.BookingAllowed(RoomType.Standard);

        // Assert
        Assert.False(bookingAllowed);
    }

    [Fact]
    public void BookingAllowedByNeitherCompanyNorEmployeeBookingPolicy()
    {
        // Arrange
        var employeeBookingPolicy = new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.JuniorSuite });
        var companyBookingPolicy = new CompanyBookingPolicy(100, new List<RoomType> { RoomType.JuniorSuite });
        var bookingPolicy = new AggregatedBookingPolicy(employeeBookingPolicy, companyBookingPolicy);

        // Act
        var bookingAllowed = bookingPolicy.BookingAllowed(RoomType.Standard);

        // Assert
        Assert.False(bookingAllowed);
    }

    [Fact]
    public void NoBookingPoliciesForAnEmployee()
    {
        // Arrange
        var employeeBookingPolicy = new NonApplicableBookingPolicy();
        var companyBookingPolicy = new NonApplicableBookingPolicy();
        var bookingPolicy = new AggregatedBookingPolicy(employeeBookingPolicy, companyBookingPolicy);

        // Act
        var bookingAllowed = bookingPolicy.BookingAllowed(RoomType.Standard);

        // Assert
        Assert.True(bookingAllowed);
    }
}