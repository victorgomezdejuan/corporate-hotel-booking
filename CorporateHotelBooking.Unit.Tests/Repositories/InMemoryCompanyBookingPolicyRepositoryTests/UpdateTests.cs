using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryCompanyBookingPolicyRepositoryTests;

public class UpdateTests
{
    [Fact]
    public void UpdateCompanyPolicy()
    {
        // Arrange
        var companyPolicyRepository = new InMemoryCompanyBookingPolicyRepository();
        var companyPolicyToBeAdded = new CompanyBookingPolicy(1, new List<RoomType> { RoomType.Standard });
        companyPolicyRepository.Add(companyPolicyToBeAdded);
        var updatedCompanyPolicy = new CompanyBookingPolicy(1, new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // Act
        companyPolicyRepository.Update(updatedCompanyPolicy);

        // Assert
        var retrievedCompanyPolicy = companyPolicyRepository.Get(1);
        retrievedCompanyPolicy.Should().Be(updatedCompanyPolicy);
    }    
}