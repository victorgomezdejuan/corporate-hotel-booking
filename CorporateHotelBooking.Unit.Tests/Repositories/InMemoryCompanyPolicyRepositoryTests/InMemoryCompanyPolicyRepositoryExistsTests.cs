using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryCompanyPolicyRepositoryTests;

public class InMemoryCompanyPolicyRepositoryExistsTests
{
    [Fact]
    public void CompanyPolicyExists()
    {
        // Arrange
        var companyPolicyRepository = new InMemoryCompanyPolicyRepository();
        companyPolicyRepository.AddCompanyPolicy(new CompanyPolicy(1, new List<RoomType> { RoomType.Standard }));

        // Act
        var companyPolicyExists = companyPolicyRepository.Exists(1);

        // Assert
        companyPolicyExists.Should().BeTrue();
    }

    [Fact]
    public void CompanyPolicyDoesNotExist()
    {
        // Arrange
        var companyPolicyRepository = new InMemoryCompanyPolicyRepository();

        // Act
        var companyPolicyExists = companyPolicyRepository.Exists(1);

        // Assert
        companyPolicyExists.Should().BeFalse();
    }
}