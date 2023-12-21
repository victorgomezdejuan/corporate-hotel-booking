using AutoFixture.Xunit2;
using CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;
using CorporateHotelBooking.Repositories.Bookings;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.Employees.Commands;

public class EmployeeBookingDeleterTests
{
    [Theory, AutoData]
    public void DeleteAllTheBookingsAssociatedToAnEmployeeWhenNotified(int employeeId)
    {
        // Arrange
        var bookingRepositoryMock = new Mock<IBookingRepository>();
        var deleter = new EmployeeBookingDeleter(bookingRepositoryMock.Object);

        // Act
        deleter.Notify(employeeId);

        // Assert
        bookingRepositoryMock.Verify(r => r.DeleteByEmployee(employeeId), Times.Once);
    }
}