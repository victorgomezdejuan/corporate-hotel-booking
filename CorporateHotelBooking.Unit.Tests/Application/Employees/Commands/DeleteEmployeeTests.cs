using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.Employees.Commands;

public class DeleteEmployeeTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly DeleteEmployeeCommandHandler _deleteEmployeeCommandHandler;

    public DeleteEmployeeTests()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        _deleteEmployeeCommandHandler = new DeleteEmployeeCommandHandler(_employeeRepositoryMock.Object);
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
}