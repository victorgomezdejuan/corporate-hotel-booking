using CorporateHotelBooking.Application.Employees.Commands;
using CorporateHotelBooking.Application.Employees.Commands.AddEmployee;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.Employees.Commands;

public class AddEmployeeTests
{
    [Fact]
    public void AddEmployee()
    {
        // Arrange
        var employeeRepository = new Mock<IEmployeeRepository>();
        var addEmployeeCommand = new AddEmployeeCommand(1, 100);
        var addEmployeeCommandHandler = new AddEmployeeCommandHandler(employeeRepository.Object);

        // Act
        addEmployeeCommandHandler.Handle(addEmployeeCommand);

        // Assert
        employeeRepository.Verify(r => r.AddEmployee(new Employee(1, 100)));
    }

    [Fact]
    public void AddExistingEmployee()
    {
        // Arrange
        var employeeRepository = new Mock<IEmployeeRepository>();
        employeeRepository.Setup(r => r.Exists(1)).Returns(true);
        var addEmployeeCommandHandler = new AddEmployeeCommandHandler(employeeRepository.Object);

        // Act
        Action action = () => addEmployeeCommandHandler.Handle(new AddEmployeeCommand(1, 100));

        // Assert
        action.Should().Throw<EmployeeAlreadyExistsException>();
    }
}