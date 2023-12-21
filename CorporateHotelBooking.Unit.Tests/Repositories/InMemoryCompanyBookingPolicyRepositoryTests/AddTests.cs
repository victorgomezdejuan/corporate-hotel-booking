using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryCompanyBookingPolicyRepositoryTests;

public class AddTests
{
    [Theory, AutoData]
    public void AddCompanyPolicy(int companyId, [CollectionSize(2)] List<RoomType> allowedRoomTypes)
    {
        // Arrange
        var repository = new InMemoryCompanyBookingPolicyRepository();
        var companyPolicyToBeAdded = new CompanyBookingPolicy(companyId, allowedRoomTypes);

        // Act
        repository.Add(companyPolicyToBeAdded);

        // Assert
        var retrievedCompanyPolicy = repository.Get(companyId);
        retrievedCompanyPolicy.Should().Be(companyPolicyToBeAdded);
    }
}