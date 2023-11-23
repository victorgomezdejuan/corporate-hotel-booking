using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests.Helpers;
using CorporateHotelBooking.Repositories.CompanyPolicies;
using CorporateHotelBooking.Repositories.EmployeePolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests;

public class SetEmployeePolicyTests
{
    private readonly InMemoryEmployeePolicyRepository _employeePolicyRepository;
    private readonly BookingPolicyService _bookingPolicyService;

    public SetEmployeePolicyTests()
    {
        _employeePolicyRepository = new InMemoryEmployeePolicyRepository();
        _bookingPolicyService = new BookingPolicyService(
            // BookingPolicyService acts as a facade that handles different actions related to booking policies
            // This leads us to feed it with two additional repositories although for this use case they are not needed
            new NotImplementedCompanyPolicyRepository(),
            _employeePolicyRepository,
            new NotImplementedEmployeeRepository());
    }

    [Fact]
    public void AddNewEmployeePolicy()
    {
        // Act
        _bookingPolicyService.SetEmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Assert
        var retrievedEmployeePolicy = _employeePolicyRepository.GetEmployeePolicy(1);
        retrievedEmployeePolicy.Should().Be(new EmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));
    }

    [Fact]
    public void UpdateExistingEmployeePolicy()
    {
        // Arrange
        _bookingPolicyService.SetEmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        _bookingPolicyService.SetEmployeePolicy(1, new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // Assert
        var retrievedEmployeePolicy = _employeePolicyRepository.GetEmployeePolicy(1);
        retrievedEmployeePolicy.Should().Be(new EmployeePolicy(1, new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite }));
    }
}

public class NotImplementedCompanyPolicyRepository : ICompanyPolicyRepository
{
    public void AddCompanyPolicy(CompanyPolicy companyPolicy)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int companyId)
    {
        throw new NotImplementedException();
    }

    public CompanyPolicy GetCompanyPolicy(int companyId)
    {
        throw new NotImplementedException();
    }

    public void UpdateCompanyPolicy(CompanyPolicy companyPolicy)
    {
        throw new NotImplementedException();
    }
}