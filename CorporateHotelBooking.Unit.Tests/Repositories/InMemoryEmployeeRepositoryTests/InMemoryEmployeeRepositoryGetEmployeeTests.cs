using CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeRepositoryTests;

public class InMemoryEmployeeRepositoryGetEmployeeTests
{
    [Fact]
    public void GetEmployee()
    {
        // Arrange
        var employeeRepository = new InMemoryEmployeeRepository();
        var employeeToBeAdded = new Employee(1, 100);
        employeeRepository.AddEmployee(employeeToBeAdded);

        // Act
        var retrievedEmployee = employeeRepository.GetEmployee(1);

        // Assert
        retrievedEmployee.Should().Be(employeeToBeAdded);
    }

    [Fact]
    public void GetNonExistingEmployee()
    {
        // Arrange
        var employeeRepository = new InMemoryEmployeeRepository();

        // Act
        Action getEmployeeAction = () => employeeRepository.GetEmployee(1);

        // Assert
        getEmployeeAction.Should().Throw<EmployeeNotFoundException>();
    }
}