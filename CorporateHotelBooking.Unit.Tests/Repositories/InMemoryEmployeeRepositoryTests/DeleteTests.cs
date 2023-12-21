using AutoFixture.Xunit2;
using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeRepositoryTests;

public class DeleteTests
{
    [Theory, AutoData]
    public void DeleteExistingEmployee(Employee employee)
    {
        // Arrange
        var repository = new InMemoryEmployeeRepository();
        repository.Add(employee);

        // Act
        repository.Delete(employee.Id);

        // Assert
        Action getEmployeeAction = () => repository.Get(employee.Id);
        getEmployeeAction.Should().Throw<EmployeeNotFoundException>();
    }
}