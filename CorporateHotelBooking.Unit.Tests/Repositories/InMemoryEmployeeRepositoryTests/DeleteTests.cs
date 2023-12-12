using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeRepositoryTests;

public class DeleteTests
{
    [Fact]
    public void DeleteExistingEmployee()
    {
        // Arrange
        var employeeRepository = new InMemoryEmployeeRepository();
        employeeRepository.Add(new Employee(1, 100));

        // Act
        employeeRepository.Delete(1);

        // Assert
        Action getEmployeeAction = () => employeeRepository.Get(1);
        getEmployeeAction.Should().Throw<EmployeeNotFoundException>();
    }
}