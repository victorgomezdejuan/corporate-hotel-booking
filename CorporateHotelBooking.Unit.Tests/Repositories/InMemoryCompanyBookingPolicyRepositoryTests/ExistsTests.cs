using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryCompanyBookingPolicyRepositoryTests;

public class ExistsTests
{
    private readonly InMemoryCompanyBookingPolicyRepository _repository;

    public ExistsTests()
    {
        _repository = new InMemoryCompanyBookingPolicyRepository();
    }

    [Theory, AutoData]
    public void CompanyPolicyExists(int companyId, [CollectionSize(1)] List<RoomType> allowedRoomTypes)
    {
        // Arrange
        _repository.Add(new CompanyBookingPolicy(companyId, allowedRoomTypes));

        // Act
        var companyPolicyExists = _repository.Exists(companyId);

        // Assert
        companyPolicyExists.Should().BeTrue();
    }

    [Theory, AutoData]
    public void CompanyPolicyDoesNotExist(int companyId)
    {
        // Act
        var companyPolicyExists = _repository.Exists(companyId);

        // Assert
        companyPolicyExists.Should().BeFalse();
    }
}