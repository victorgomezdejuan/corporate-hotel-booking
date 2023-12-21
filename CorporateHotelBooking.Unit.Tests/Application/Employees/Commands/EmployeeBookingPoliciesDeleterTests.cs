using AutoFixture.Xunit2;
using CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.Employees.Commands;

public class EmployeeBookingPoliciesDeleterTests
{
    [Theory, AutoData]
    public void DeleteAllTheBookingPoliciesAssociatedToAnEmployeeWhenNotified(int employeeId)
    {
        // Arrange
        var employeeBookingPolicyRepositoryMock = new Mock<IEmployeeBookingPolicyRepository>();
        var deleter = new EmployeeBookingPoliciesDeleter(employeeBookingPolicyRepositoryMock.Object);

        // Act
        deleter.Notify(employeeId);

        // Assert
        employeeBookingPolicyRepositoryMock.Verify(r => r.Delete(employeeId), Times.Once);
    }
}