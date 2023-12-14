using CorporateHotelBooking.Application.Employees.Commands.AddEmployee;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using CorporateHotelBooking.Services;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.CompanyServiceTests;

public class AddEmployeeTests
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IEmployeeBookingPolicyRepository _employeeBookingPolicyRepository;
    private readonly CompanyService _companyService;

    public AddEmployeeTests()
    {
        _employeeRepository = new InMemoryEmployeeRepository();
        _bookingRepository = new InMemoryBookingRepository();
        _employeeBookingPolicyRepository = new InMemoryEmployeeBookingPolicyRepository();
        _companyService = new CompanyService(_employeeRepository, _bookingRepository, _employeeBookingPolicyRepository);
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