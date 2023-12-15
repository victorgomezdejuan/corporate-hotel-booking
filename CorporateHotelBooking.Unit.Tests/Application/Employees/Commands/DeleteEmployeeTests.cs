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

    [Fact]
    public void DeleteExistingEmployee()
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(1)).Returns(true);

        // Act
        _deleteEmployeeCommandHandler.Handle(new DeleteEmployeeCommand(1));

        // Assert
        _employeeRepositoryMock.Verify(r => r.Delete(1));
    }

    [Fact]
    public void DeleteNonExistingEmployee()
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(1)).Returns(false);

        // Act
        Action action = () => _deleteEmployeeCommandHandler.Handle(new DeleteEmployeeCommand(1));

        // Assert
        action.Should().Throw<EmployeeNotFoundException>();
    }

    [Fact]
    public void DeleteEmployeeBookingsWhenDeletingEmployee()
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(1)).Returns(true);

        // Act
        _deleteEmployeeCommandHandler.Handle(new DeleteEmployeeCommand(1));

        // Assert
        _bookingRepositoryMock.Verify(r => r.DeleteByEmployee(1));
    }

    [Fact]
    public void DeleteEmployeeBookingPoliciesWhenDeletingEmployee()
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(1)).Returns(true);

        // Act
        _deleteEmployeeCommandHandler.Handle(new DeleteEmployeeCommand(1));

        // Assert
        _employeeBookingPolicyRepositoryMock.Verify(r => r.Delete(1));
    }
}