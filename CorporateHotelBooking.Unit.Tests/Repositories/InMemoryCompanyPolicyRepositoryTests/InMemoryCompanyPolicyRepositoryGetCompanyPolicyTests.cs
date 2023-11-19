using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryCompanyPolicyRepositoryTests;

public class InMemoryCompanyPolicyRepositoryGetCompanyPolicyTests
{
    [Fact]
    public void GetExistingCompanyPolicy()
    {
        // Arrange
        var companyPolicyRepository = new InMemoryCompanyPolicyRepository();
        var companyPolicyToBeAdded = new CompanyPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });
        companyPolicyRepository.AddCompanyPolicy(companyPolicyToBeAdded);

        // Act
        var retrievedCompanyPolicy = companyPolicyRepository.GetCompanyPolicy(1);

        // Assert
        retrievedCompanyPolicy.Should().Be(companyPolicyToBeAdded);
    }
}