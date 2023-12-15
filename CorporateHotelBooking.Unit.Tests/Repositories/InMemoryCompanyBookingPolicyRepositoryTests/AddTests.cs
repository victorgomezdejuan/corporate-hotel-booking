using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryCompanyBookingPolicyRepositoryTests;

public class AddTests
{
    [Fact]
    public void AddCompanyPolicy()
    {
        // Arrange
        var repository = new InMemoryCompanyBookingPolicyRepository();
        var companyPolicyToBeAdded = new CompanyBookingPolicy(
            1,
            new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        repository.Add(companyPolicyToBeAdded);

        // Assert
        var retrievedCompanyPolicy = repository.Get(1);
        retrievedCompanyPolicy.Should().Be(companyPolicyToBeAdded);
    }
}