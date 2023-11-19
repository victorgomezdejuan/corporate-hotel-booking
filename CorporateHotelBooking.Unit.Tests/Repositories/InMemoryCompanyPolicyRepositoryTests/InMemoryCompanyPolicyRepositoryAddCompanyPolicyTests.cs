using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryCompanyPolicyRepositoryTests;

public class InMemoryCompanyPolicyRepositoryAddCompanyPolicyTests
{
    [Fact]
    public void AddCompanyPolicy()
    {
        // Arrange
        var companyPolicyRepository = new InMemoryCompanyPolicyRepository();
        var companyPolicyToBeAdded = new CompanyPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        companyPolicyRepository.AddCompanyPolicy(companyPolicyToBeAdded);

        // Assert
        var retrievedCompanyPolicy = companyPolicyRepository.GetCompanyPolicy(1);
        retrievedCompanyPolicy.Should().Be(companyPolicyToBeAdded);
    }
}