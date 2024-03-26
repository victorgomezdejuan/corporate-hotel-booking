using AutoFixture.Xunit2;
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

    [Theory, AutoData]
    public void AddEmployee(int companyId, int employeeId)
    {
        // Act
        var result = _companyService.AddEmployee(companyId, employeeId);

        // Assert
        result.IsFailure.Should().BeFalse();
        var employee = _employeeRepository.Get(employeeId);
        employee!.Id.Should().Be(employeeId);
        employee.CompanyId.Should().Be(companyId);
    }

    [Theory, AutoData]
    public void AddExistingEmployee(int companyId, int employeeId)
    {
        // Arrange
        _companyService.AddEmployee(companyId, employeeId);

        // Act
        var result = _companyService.AddEmployee(companyId, employeeId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Employee already exists in the company");
    }
}