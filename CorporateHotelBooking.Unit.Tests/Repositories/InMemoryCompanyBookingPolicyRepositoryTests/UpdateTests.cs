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
        var repository = new InMemoryCompanyBookingPolicyRepository();

        var companyPolicyToBeAdded = new CompanyBookingPolicy(
            1,
            new List<RoomType> { RoomType.Standard });
        repository.Add(companyPolicyToBeAdded);
        
        var updatedCompanyPolicy = new CompanyBookingPolicy(
            1,
            new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // Act
        repository.Update(updatedCompanyPolicy);

        // Assert
        var retrievedCompanyPolicy = repository.Get(1);
        retrievedCompanyPolicy.Should().Be(updatedCompanyPolicy);
    }    
}