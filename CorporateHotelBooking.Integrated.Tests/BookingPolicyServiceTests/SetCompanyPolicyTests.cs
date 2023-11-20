using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;
using CorporateHotelBooking.Repositories.EmployeePolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests;

public class SetCompanyPolicyTests
{
    [Fact]
    public void AddNewCompanyPolicy()
    {
        // Arrange
        var companyPolicyRepository = new InMemoryCompanyPolicyRepository();
        var bookingPolicyService = new BookingPolicyService(
            companyPolicyRepository,
            new NotImplementedEmployeePolicyRepository()); // BookingPolicyService acts as a facade that handles Company policies as well as Employee policies
            // This leads us to feed it with both repositories although for this test only one is needed
        var companyPolicyToBeAdded = new CompanyPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite  });

        // Act
        bookingPolicyService.SetCompanyPolicy(companyPolicyToBeAdded.CompanyId, companyPolicyToBeAdded.AllowedRoomTypes.ToList());

        // Assert
        var retrievedCompanyPolicy = companyPolicyRepository.GetCompanyPolicy(100);
        retrievedCompanyPolicy.Should().Be(companyPolicyToBeAdded);
    }

    [Fact]
    public void UpdateExistingCompanyPolicy()
    {
        // Arrange
        var companyPolicyRepository = new InMemoryCompanyPolicyRepository();
        var bookingPolicyService = new BookingPolicyService(
            companyPolicyRepository,
            new NotImplementedEmployeePolicyRepository()); // BookingPolicyService acts as a facade that handles Company policies as well as Employee policies
            // This leads us to feed it with both repositories although for this test only one is needed
        var addedPolicy = new CompanyPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });
        bookingPolicyService.SetCompanyPolicy(addedPolicy.CompanyId, addedPolicy.AllowedRoomTypes.ToList());
        var updatedPolicy = new CompanyPolicy(100, new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // Act
        bookingPolicyService.SetCompanyPolicy(updatedPolicy.CompanyId, updatedPolicy.AllowedRoomTypes.ToList());

        // Assert
        var retrievedCompanyPolicy = companyPolicyRepository.GetCompanyPolicy(100);
        retrievedCompanyPolicy.Should().Be(updatedPolicy);
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