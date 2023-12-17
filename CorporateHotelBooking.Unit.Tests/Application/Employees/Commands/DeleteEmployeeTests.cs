using AutoFixture.Xunit2;
using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.Employees.Commands;

public class DeleteEmployeeTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly Mock<IBookingRepository> _bookingRepositoryMock;
    private readonly Mock<IEmployeeBookingPolicyRepository> _employeeBookingPolicyRepositoryMock;
    private readonly DeleteEmployeeCommandHandler _deleteEmployeeCommandHandler;

    public DeleteEmployeeTests()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        _bookingRepositoryMock = new Mock<IBookingRepository>();
        _employeeBookingPolicyRepositoryMock = new Mock<IEmployeeBookingPolicyRepository>();
        _deleteEmployeeCommandHandler = new DeleteEmployeeCommandHandler(
            _employeeRepositoryMock.Object,
            _bookingRepositoryMock.Object,
            _employeeBookingPolicyRepositoryMock.Object);
    }

    [Theory, AutoData]
    public void DeleteExistingEmployee(int employeeId)
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(employeeId)).Returns(true);

        // Act
        _deleteEmployeeCommandHandler.Handle(new DeleteEmployeeCommand(employeeId));

        // Assert
        _employeeRepositoryMock.Verify(r => r.Delete(employeeId));
    }

    [Theory, AutoData]
    public void DeleteNonExistingEmployee(int employeeId)
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(employeeId)).Returns(false);

        // Act
        Action action = () => _deleteEmployeeCommandHandler.Handle(new DeleteEmployeeCommand(employeeId));

        // Assert
        action.Should().Throw<EmployeeNotFoundException>();
    }

    [Theory, AutoData]
    public void DeleteEmployeeBookingsWhenDeletingEmployee(int employeeId)
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(employeeId)).Returns(true);

        // Act
        _deleteEmployeeCommandHandler.Handle(new DeleteEmployeeCommand(employeeId));

        // Assert
        _bookingRepositoryMock.Verify(r => r.DeleteByEmployee(employeeId));
    }

    [Theory, AutoData]
    public void DeleteEmployeeBookingPoliciesWhenDeletingEmployee(int employeeId)
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(employeeId)).Returns(true);

        // Act
        _deleteEmployeeCommandHandler.Handle(new DeleteEmployeeCommand(employeeId));

        // Assert
        _employeeBookingPolicyRepositoryMock.Verify(r => r.Delete(employeeId));
    }
}