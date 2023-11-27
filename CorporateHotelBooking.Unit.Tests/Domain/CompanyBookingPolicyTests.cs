using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Unit.Tests.Domain;

public class CompanyBookingPolicyTests
{
    [Fact]
    public void BookingAllowed()
    {
        // Arrange
        var companyBookingPolicy = new CompanyBookingPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        var bookingAllowed = companyBookingPolicy.BookingAllowed(RoomType.Standard);

        // Assert
        Assert.True(bookingAllowed);
    }

    [Fact]
    public void BookingNotAllowed()
    {
        // Arrange
        var companyBookingPolicy = new CompanyBookingPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        var bookingAllowed = companyBookingPolicy.BookingAllowed(RoomType.MasterSuite);

        // Assert
        Assert.False(bookingAllowed);
    }
}