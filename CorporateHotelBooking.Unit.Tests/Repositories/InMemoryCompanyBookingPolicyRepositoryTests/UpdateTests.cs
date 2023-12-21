using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryCompanyBookingPolicyRepositoryTests;

public class UpdateTests
{
    [Theory, AutoData]
    public void UpdateCompanyPolicy(int companyId)
    {
        // Arrange
        var repository = new InMemoryCompanyBookingPolicyRepository();

        var companyPolicyToBeAdded = new CompanyBookingPolicy(
            companyId,
            new List<RoomType> { RoomType.Standard });
        repository.Add(companyPolicyToBeAdded);
        
        var updatedCompanyPolicy = new CompanyBookingPolicy(
            companyId,
            new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // Act
        repository.Update(updatedCompanyPolicy);

        // Assert
        var retrievedCompanyPolicy = repository.Get(companyId);
        retrievedCompanyPolicy.Should().Be(updatedCompanyPolicy);
    }    
}