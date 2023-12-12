using CorporateHotelBooking.Application.Employees.Commands.AddEmployee;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.CompanyServiceTests;

public class AddEmployeeTests
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly CompanyService _companyService;

    public AddEmployeeTests()
    {
        _employeeRepository = new InMemoryEmployeeRepository();
        _companyService = new CompanyService(_employeeRepository);
    }

    [Fact]
    public void AddEmployee()
    {
        // Act
        _companyService.AddEmployee(companyId: 100, employeeId: 1);

        // Assert
        var employee = _employeeRepository.Get(1);
        employee.Id.Should().Be(1);
        employee.CompanyId.Should().Be(100);
    }

    [Fact]
    public void AddExistingEmployee()
    {
        // Arrange
        _companyService.AddEmployee(companyId: 100, employeeId: 1);

        // Act
        Action action = () => _companyService.AddEmployee(companyId: 100, employeeId: 1);

        // Assert
        action.Should().Throw<EmployeeAlreadyExistsException>();
    }
}