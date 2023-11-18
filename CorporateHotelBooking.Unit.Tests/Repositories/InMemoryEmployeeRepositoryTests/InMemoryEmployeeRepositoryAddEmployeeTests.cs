using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeRepositoryTest;

public class InMemoryEmployeeRepositoryAddEmployeeTests
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
}