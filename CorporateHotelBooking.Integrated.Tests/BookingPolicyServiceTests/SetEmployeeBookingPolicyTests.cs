using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Integrated.Tests.Helpers;
using CorporateHotelBooking.Integrated.Tests.Helpers.AutoFixture;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using CorporateHotelBooking.Services;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests;

public class SetEmployeeBookingPolicyTests
{
    private readonly InMemoryEmployeeBookingPolicyRepository _employeePolicyRepository;
    private readonly InMemoryEmployeeRepository _employeeRepository;
    private readonly BookingPolicyService _bookingPolicyService;

    public SetEmployeeBookingPolicyTests()
    {
        _employeeRepository = new InMemoryEmployeeRepository();
        _employeePolicyRepository = new InMemoryEmployeeBookingPolicyRepository();
        _bookingPolicyService = new BookingPolicyService(
            // BookingPolicyService acts as a facade that handles different actions related to booking policies
            // This leads us to feed it with two additional repositories although for this use case they are not needed
            new NotImplementedCompanyBookingPolicyRepository(),
            _employeePolicyRepository,
            _employeeRepository);
    }

    [Theory, AutoData]
    public void AddNewEmployeePolicy(Employee employee, [CollectionSize(2)] List<RoomType> roomTypes)
    {
        // Act
        _employeeRepository.Add(employee);
        _bookingPolicyService.SetEmployeePolicy(employee.Id, roomTypes);

        // Assert
        var retrievedEmployeePolicy = _employeePolicyRepository.Get(employee.Id);
        retrievedEmployeePolicy.Should().Be(new EmployeeBookingPolicy(employee.Id, roomTypes));
    }

    [Theory, AutoData]
    public void UpdateExistingEmployeePolicy(Employee employee)
    {
        // Arrange
        _employeeRepository.Add(employee);
        _bookingPolicyService.SetEmployeePolicy(
            employee.Id,
            new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        _bookingPolicyService.SetEmployeePolicy(
            employee.Id,
            new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // Assert
        var retrievedEmployeePolicy = _employeePolicyRepository.Get(employee.Id);
        retrievedEmployeePolicy.Should().Be(new EmployeeBookingPolicy(
            employee.Id,
            new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite }));
    }
}

public class NotImplementedCompanyBookingPolicyRepository : ICompanyBookingPolicyRepository
{
    public void Add(CompanyBookingPolicy companyPolicy)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int companyId)
    {
        throw new NotImplementedException();
    }

    public CompanyBookingPolicy Get(int companyId)
    {
        throw new NotImplementedException();
    }

    public void Update(CompanyBookingPolicy companyPolicy)
    {
        throw new NotImplementedException();
    }
}