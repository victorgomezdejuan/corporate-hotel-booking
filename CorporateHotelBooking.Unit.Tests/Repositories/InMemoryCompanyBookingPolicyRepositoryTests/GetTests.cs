using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryCompanyBookingPolicyRepositoryTests;

public class GetTests
{
    [Fact]
    public void GetExistingCompanyPolicy()
    {
        // Arrange
        var repository = new InMemoryCompanyBookingPolicyRepository();
        var companyPolicyToBeAdded = new CompanyBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });
        repository.Add(companyPolicyToBeAdded);

        // Act
        var retrievedCompanyPolicy = repository.Get(1);

        // Assert
        retrievedCompanyPolicy.Should().Be(companyPolicyToBeAdded);
    }
}