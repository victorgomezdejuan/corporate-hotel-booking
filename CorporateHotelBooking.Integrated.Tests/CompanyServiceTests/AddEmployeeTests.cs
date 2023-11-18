using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.CompanyServiceTests;

public class AddEmployeeTests
{
    [Fact]
    public void AddEmployee()
    {
        // Arrange
        IEmployeeRepository employeeRepository = new InMemoryEmployeeRepository();
        var companyService = new CompanyService(employeeRepository);

        // Act
        companyService.AddEmployee(companyId: 100, employeeId: 1);

        // Assert
        var employee = employeeRepository.GetEmployee(1);
        employee.Id.Should().Be(1);
        employee.CompanyId.Should().Be(100);
    }
}