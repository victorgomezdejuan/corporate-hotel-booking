using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeRepositoryTest;

public class AddTests
{
    [Theory, AutoData]
    public void AddEmployee(Employee employee)
    {
        // Arrange
        var repository = new InMemoryEmployeeRepository();

        // Act
        repository.Add(employee);

        // Assert
        var retrievedEmployee = repository.Get(employee.Id);
        retrievedEmployee.Should().Be(employee);
    }
}