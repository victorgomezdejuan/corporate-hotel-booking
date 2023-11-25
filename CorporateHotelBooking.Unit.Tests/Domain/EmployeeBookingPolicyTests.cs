using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Unit.Tests.Domain;

public class EmployeeBookingPolicyTests
{
    [Fact]
    public void BookingAllowed()
    {
        // Arrange
        var employeeBookingPolicy = new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        var bookingAllowed = employeeBookingPolicy.BookingAllowed(RoomType.Standard);

        // Assert
        Assert.True(bookingAllowed);
    }

    [Fact]
    public void BookingNotAllowed()
    {
        // Arrange
        var employeeBookingPolicy = new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        var bookingAllowed = employeeBookingPolicy.BookingAllowed(RoomType.MasterSuite);

        // Assert
        Assert.False(bookingAllowed);
    }
}