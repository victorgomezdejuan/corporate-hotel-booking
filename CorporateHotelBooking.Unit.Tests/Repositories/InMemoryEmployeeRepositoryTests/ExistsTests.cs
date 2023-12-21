using AutoFixture.Xunit2;
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

    [Theory, AutoData]
    public void EmployeeExists(Employee employee)
    {
        // Arrange
        _repository.Add(employee);

        // Act
        var employeeExists = _repository.Exists(employee.Id);

        // Assert
        employeeExists.Should().BeTrue();
    }

    [Theory, AutoData]
    public void EmployeeDoesNotExist(int employeeId)
    {
        // Act
        var employeeExists = _repository.Exists(employeeId);

        // Assert
        employeeExists.Should().BeFalse();
    }
}