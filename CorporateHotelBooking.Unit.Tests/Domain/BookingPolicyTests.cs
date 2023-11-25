using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Unit.Tests.Domain;

public class BookingPolicyTests
{
    [Fact]
    public void BookingAllowedByCompanyBookingPolicy()
    {
        // Arrange
        var employeeBookingPolicy = new EmployeeBookingPolicy(1, new List<RoomType> { });
        var companyBookingPolicy = new CompanyBookingPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });
        var bookingPolicy = new BookingPolicy(employeeBookingPolicy, companyBookingPolicy);

        // Act
        var bookingAllowed = bookingPolicy.BookingAllowed(RoomType.Standard);

        // Assert
        Assert.True(bookingAllowed);
    }

    // Add the rest of the tests equivalent to the ones in IsBookingAllowedTests
}