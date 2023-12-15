using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryCompanyBookingPolicyRepositoryTests;

public class ExistsTests
{
    private readonly InMemoryCompanyBookingPolicyRepository _repository;

    public ExistsTests()
    {
        _repository = new InMemoryCompanyBookingPolicyRepository();
    }

    [Fact]
    public void CompanyPolicyExists()
    {
        // Arrange
        _repository.Add(new CompanyBookingPolicy(1, new List<RoomType> { RoomType.Standard }));

        // Act
        var companyPolicyExists = _repository.Exists(1);

        // Assert
        companyPolicyExists.Should().BeTrue();
    }

    [Fact]
    public void CompanyPolicyDoesNotExist()
    {
        // Act
        var companyPolicyExists = _repository.Exists(1);

        // Assert
        companyPolicyExists.Should().BeFalse();
    }
}