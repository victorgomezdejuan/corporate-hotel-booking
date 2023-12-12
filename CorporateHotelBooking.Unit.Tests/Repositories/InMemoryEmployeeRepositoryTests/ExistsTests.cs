using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeRepositoryTests;

public class ExistsTests
{
    private readonly InMemoryEmployeeRepository _employeeRepository;

    public ExistsTests()
    {
        _employeeRepository = new InMemoryEmployeeRepository();
    }

    [Fact]
    public void EmployeeExists()
    {
        // Arrange
        var employeeToBeAdded = new Employee(1, 100);
        _employeeRepository.Add(employeeToBeAdded);

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