using AutoFixture.Xunit2;
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

    [Theory, AutoData]
    public void GetEmployee(Employee employee)
    {
        // Arrange
        _repository.Add(employee);

        // Act
        var retrievedEmployee = _repository.Get(employee.Id);

        // Assert
        retrievedEmployee.Should().Be(employee);
    }

    [Theory, AutoData]
    public void GetNonExistingEmployee(int employeeId)
    {
        _repository.Get(employeeId).Should().BeNull();
    }
}