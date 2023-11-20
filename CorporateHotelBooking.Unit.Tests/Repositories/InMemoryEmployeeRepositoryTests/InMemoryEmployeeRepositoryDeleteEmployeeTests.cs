using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeRepositoryTests;

public class InMemoryEmployeeRepositoryDeleteEmployeeTests
{
    [Fact]
    public void DeleteExistingEmployee()
    {
        // Arrange
        var employeeRepository = new InMemoryEmployeeRepository();
        employeeRepository.AddEmployee(new Employee(1, 100));

        // Act
        employeeRepository.DeleteEmployee(1);

        // Assert
        Action getEmployeeAction = () => employeeRepository.GetEmployee(1);
        getEmployeeAction.Should().Throw<EmployeeNotFoundException>();
    }
}