using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeRepositoryTests;

public class InMemoryEmployeeRepositoryExistsTests
{
    [Fact]
    public void EmployeeExists()
    {
        // Arrange
        var employeeRepository = new InMemoryEmployeeRepository();
        var employeeToBeAdded = new Employee(1, 100);
        employeeRepository.AddEmployee(employeeToBeAdded);

        // Act
        var employeeExists = employeeRepository.Exists(1);

        // Assert
        employeeExists.Should().BeTrue();
    }

    [Fact]
    public void EmployeeDoesNotExist()
    {
        // Arrange
        var employeeRepository = new InMemoryEmployeeRepository();

        // Act
        var employeeExists = employeeRepository.Exists(1);

        // Assert
        employeeExists.Should().BeFalse();
    }
}