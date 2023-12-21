using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryCompanyBookingPolicyRepositoryTests;

public class GetTests
{
    [Theory, AutoData]
    public void GetExistingCompanyPolicy(int companyId, [CollectionSize(2)] List<RoomType> allowedRoomTypes)
    {
        // Arrange
        var repository = new InMemoryCompanyBookingPolicyRepository();
        var companyPolicyToBeAdded = new CompanyBookingPolicy(companyId, allowedRoomTypes);
        repository.Add(companyPolicyToBeAdded);

        // Act
        var retrievedCompanyPolicy = repository.Get(companyId);

        // Assert
        retrievedCompanyPolicy.Should().Be(companyPolicyToBeAdded);
    }
}