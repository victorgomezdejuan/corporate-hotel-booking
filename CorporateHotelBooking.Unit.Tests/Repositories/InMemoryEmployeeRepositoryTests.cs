using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories;

public class InMemoryEmployeeRepositoryTests
{
    [Fact]
    public void AddEmployee()
    {
        // Arrange
        var employeeRepository = new InMemoryEmployeeRepository();
        var employeeToBeAdded = new Employee(1, 100);

        // Act
        employeeRepository.AddEmployee(employeeToBeAdded);

        // Assert
        var retrievedEmployee = employeeRepository.GetEmployee(1);
        retrievedEmployee.Should().Be(employeeToBeAdded);
    }

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
}