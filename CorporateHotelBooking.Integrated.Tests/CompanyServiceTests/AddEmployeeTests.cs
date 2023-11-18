using CorporateHotelBooking.Application.Employees.Commands;
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

    [Fact]
    public void AddExistingEmployee()
    {
        // Arrange
        IEmployeeRepository employeeRepository = new InMemoryEmployeeRepository();
        var companyService = new CompanyService(employeeRepository);
        companyService.AddEmployee(companyId: 100, employeeId: 1);

        // Act
        Action action = () => companyService.AddEmployee(companyId: 100, employeeId: 1);

        // Assert
        action.Should().Throw<EmployeeAlreadyExistsException>();
    }
}