using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryCompanyBookingPolicyRepositoryTests;

public class InMemoryCompanyBookingPolicyRepositoryUpdateCompanyPolicyTests
{
    [Fact]
    public void UpdateCompanyPolicy()
    {
        // Arrange
        var companyPolicyRepository = new InMemoryCompanyBookingPolicyRepository();
        var companyPolicyToBeAdded = new CompanyBookingPolicy(1, new List<RoomType> { RoomType.Standard });
        companyPolicyRepository.AddCompanyPolicy(companyPolicyToBeAdded);
        var updatedCompanyPolicy = new CompanyBookingPolicy(1, new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // Act
        companyPolicyRepository.UpdateCompanyPolicy(updatedCompanyPolicy);

        // Assert
        var retrievedCompanyPolicy = companyPolicyRepository.GetCompanyPolicy(1);
        retrievedCompanyPolicy.Should().Be(updatedCompanyPolicy);
    }    
}