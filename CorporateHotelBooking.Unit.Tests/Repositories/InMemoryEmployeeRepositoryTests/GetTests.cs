using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeRepositoryTests;

public class GetTests
{
    private readonly InMemoryEmployeeRepository _repository;

    public GetTests()
    {
        _repository = new InMemoryEmployeeRepository();
    }

    [Fact]
    public void GetEmployee()
    {
        // Arrange
        var employeeToBeAdded = new Employee(1, 100);
        _repository.Add(employeeToBeAdded);

        // Act
        var retrievedEmployee = _repository.Get(1);

        // Assert
        retrievedEmployee.Should().Be(employeeToBeAdded);
    }

    [Fact]
    public void GetNonExistingEmployee()
    {
        // Act
        Action getAction = () => _repository.Get(1);

        // Assert
        getAction.Should().Throw<EmployeeNotFoundException>();
    }
}