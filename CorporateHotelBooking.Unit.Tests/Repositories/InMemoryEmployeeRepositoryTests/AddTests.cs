using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeRepositoryTest;

public class AddTests
{
    [Fact]
    public void AddEmployee()
    {
        // Arrange
        var repository = new InMemoryEmployeeRepository();
        var employeeToBeAdded = new Employee(1, 100);

        // Act
        repository.Add(employeeToBeAdded);

        // Assert
        var retrievedEmployee = repository.Get(1);
        retrievedEmployee.Should().Be(employeeToBeAdded);
    }
}