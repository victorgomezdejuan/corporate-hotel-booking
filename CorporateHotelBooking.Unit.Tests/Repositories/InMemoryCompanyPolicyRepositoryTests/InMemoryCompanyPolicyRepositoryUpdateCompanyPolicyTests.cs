using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryCompanyPolicyRepositoryTests;

public class InMemoryCompanyPolicyRepositoryUpdateCompanyPolicyTests
{
    [Fact]
    public void UpdateCompanyPolicy()
    {
        // Arrange
        var companyPolicyRepository = new InMemoryCompanyPolicyRepository();
        var companyPolicyToBeAdded = new CompanyPolicy(1, new List<RoomType> { RoomType.Standard });
        companyPolicyRepository.AddCompanyPolicy(companyPolicyToBeAdded);
        var updatedCompanyPolicy = new CompanyPolicy(1, new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // Act
        companyPolicyRepository.UpdateCompanyPolicy(updatedCompanyPolicy);

        // Assert
        var retrievedCompanyPolicy = companyPolicyRepository.GetCompanyPolicy(1);
        retrievedCompanyPolicy.Should().Be(updatedCompanyPolicy);
    }    
}