using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;
using CorporateHotelBooking.Repositories.EmployeePolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests;

public class SetCompanyPolicyTests
{
    private readonly InMemoryCompanyPolicyRepository _companyPolicyRepository;
    private readonly BookingPolicyService _bookingPolicyService;

    public SetCompanyPolicyTests()
    {
        _companyPolicyRepository = new InMemoryCompanyPolicyRepository();
        _bookingPolicyService = new BookingPolicyService(
            _companyPolicyRepository,
            new NotImplementedEmployeePolicyRepository()); // BookingPolicyService acts as a facade that handles Company policies as well as Employee policies
            // This leads us to feed it with both repositories although for this test only one is needed
    }

    [Fact]
    public void AddNewCompanyPolicy()
    {
        // Act
        _bookingPolicyService.SetCompanyPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Assert
        var retrievedCompanyPolicy = _companyPolicyRepository.GetCompanyPolicy(100);
        retrievedCompanyPolicy.Should().Be(new CompanyPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));
    }

    [Fact]
    public void UpdateExistingCompanyPolicy()
    {
        // Arrange
        _bookingPolicyService.SetCompanyPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        _bookingPolicyService.SetCompanyPolicy(100, new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // Assert
        var retrievedCompanyPolicy = _companyPolicyRepository.GetCompanyPolicy(100);
        retrievedCompanyPolicy.Should().Be(new CompanyPolicy(100, new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite }));
    }
}

public class NotImplementedEmployeePolicyRepository : IEmployeePolicyRepository
{
    public void AddEmployeePolicy(EmployeePolicy employeePolicy)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int employeeId)
    {
        throw new NotImplementedException();
    }

    public EmployeePolicy GetEmployeePolicy(int employeeId)
    {
        throw new NotImplementedException();
    }

    public void UpdateEmployeePolicy(EmployeePolicy employeePolicy)
    {
        throw new NotImplementedException();
    }
}