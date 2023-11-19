using CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.Employees.Commands;

public class DeleteEmployeeTests
{
    [Fact]
    public void DeleteExistingEmployee()
    {
        // Arrange
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        employeeRepositoryMock.Setup(r => r.Exists(1)).Returns(true);
        var deleteEmployeeCommandHandler = new DeleteEmployeeCommandHandler(employeeRepositoryMock.Object);

        // Act
        deleteEmployeeCommandHandler.Handle(new DeleteEmployeeCommand(1));

        // Assert
        employeeRepositoryMock.Verify(r => r.DeleteEmployee(1));
    }

    [Fact]
    public void DeleteNonExistingEmployee()
    {
        // Arrange
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        employeeRepositoryMock.Setup(r => r.Exists(1)).Returns(false);
        var deleteEmployeeCommandHandler = new DeleteEmployeeCommandHandler(employeeRepositoryMock.Object);

        // Act
        Action action = () => deleteEmployeeCommandHandler.Handle(new DeleteEmployeeCommand(1));

        // Assert
        action.Should().Throw<EmployeeNotFoundException>();
    }
}