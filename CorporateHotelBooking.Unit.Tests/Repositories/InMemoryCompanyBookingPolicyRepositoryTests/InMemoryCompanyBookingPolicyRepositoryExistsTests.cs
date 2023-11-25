using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryCompanyBookingPolicyRepositoryTests;

public class InMemoryCompanyBookingPolicyRepositoryExistsTests
{
    [Fact]
    public void CompanyPolicyExists()
    {
        // Arrange
        var companyPolicyRepository = new InMemoryCompanyBookingPolicyRepository();
        companyPolicyRepository.AddCompanyPolicy(new CompanyBookingPolicy(1, new List<RoomType> { RoomType.Standard }));

        // Act
        var companyPolicyExists = companyPolicyRepository.Exists(1);

        // Assert
        companyPolicyExists.Should().BeTrue();
    }

    [Fact]
    public void CompanyPolicyDoesNotExist()
    {
        // Arrange
        var companyPolicyRepository = new InMemoryCompanyBookingPolicyRepository();

        // Act
        var companyPolicyExists = companyPolicyRepository.Exists(1);

        // Assert
        companyPolicyExists.Should().BeFalse();
    }
}