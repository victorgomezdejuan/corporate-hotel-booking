using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;
using CorporateHotelBooking.Repositories.EmployeePolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests;

public class SetEmployeePolicyTests
{
    [Fact]
    public void AddNewEmployeePolicy()
    {
        // Arrange
        var employeePolicyRepository = new InMemoryEmployeePolicyRepository();
        var bookingPolicyService = new BookingPolicyService(
            new NotImplementedCompanyPolicyRepository(), // BookingPolicyService acts as a facade that handles Company policies as well as Employee policies
            // This leads us to feed it with both repositories although for this test only one is needed
            employeePolicyRepository);
        var employeePolicyToBeAdded = new EmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite  });

        // Act
        bookingPolicyService.SetEmployeePolicy(employeePolicyToBeAdded.EmployeeId, employeePolicyToBeAdded.AllowedRoomTypes.ToList());

        // Assert
        var retrievedEmployeePolicy = employeePolicyRepository.GetEmployeePolicy(1);
        retrievedEmployeePolicy.Should().Be(employeePolicyToBeAdded);
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