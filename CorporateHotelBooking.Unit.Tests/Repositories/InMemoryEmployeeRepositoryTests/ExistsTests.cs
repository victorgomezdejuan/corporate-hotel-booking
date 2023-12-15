using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeRepositoryTests;

public class ExistsTests
{
    private readonly InMemoryEmployeeRepository _repository;

    public ExistsTests()
    {
        _repository = new InMemoryEmployeeRepository();
    }

    [Fact]
    public void EmployeeExists()
    {
        // Arrange
        var employeeToBeAdded = new Employee(1, 100);
        _repository.Add(employeeToBeAdded);

        // Act
        var employeeExists = _repository.Exists(1);

        // Assert
        employeeExists.Should().BeTrue();
    }

    [Fact]
    public void EmployeeDoesNotExist()
    {
        // Act
        var employeeExists = _repository.Exists(1);

        // Assert
        employeeExists.Should().BeFalse();
    }
}