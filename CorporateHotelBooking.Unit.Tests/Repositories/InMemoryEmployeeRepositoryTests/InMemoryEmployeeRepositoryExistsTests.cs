using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeRepositoryTests;

public class InMemoryEmployeeRepositoryExistsTests
{
    private readonly InMemoryEmployeeRepository _employeeRepository;

    public InMemoryEmployeeRepositoryExistsTests()
    {
        _employeeRepository = new InMemoryEmployeeRepository();
    }

    [Fact]
    public void EmployeeExists()
    {
        // Arrange
        var employeeToBeAdded = new Employee(1, 100);
        _employeeRepository.AddEmployee(employeeToBeAdded);

        // Act
        var employeeExists = _employeeRepository.Exists(1);

        // Assert
        employeeExists.Should().BeTrue();
    }

    [Fact]
    public void EmployeeDoesNotExist()
    {
        // Act
        var employeeExists = _employeeRepository.Exists(1);

        // Assert
        employeeExists.Should().BeFalse();
    }
}