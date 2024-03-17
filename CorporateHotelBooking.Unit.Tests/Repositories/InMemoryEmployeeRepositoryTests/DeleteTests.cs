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
        repository.Get(employee.Id).Should().BeNull();
    }
}