using CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeRepositoryTests;

public class InMemoryEmployeeRepositoryGetEmployeeTests
{
    private readonly InMemoryEmployeeRepository _employeeRepository;

    public InMemoryEmployeeRepositoryGetEmployeeTests()
    {
        _employeeRepository = new InMemoryEmployeeRepository();
    }

    [Fact]
    public void GetEmployee()
    {
        // Arrange
        var employeeToBeAdded = new Employee(1, 100);
        _employeeRepository.AddEmployee(employeeToBeAdded);

        // Act
        var retrievedEmployee = _employeeRepository.GetEmployee(1);

        // Assert
        retrievedEmployee.Should().Be(employeeToBeAdded);
    }

    [Fact]
    public void GetNonExistingEmployee()
    {
        // Act
        Action getEmployeeAction = () => _employeeRepository.GetEmployee(1);

        // Assert
        getEmployeeAction.Should().Throw<EmployeeNotFoundException>();
    }
}