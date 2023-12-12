using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeBookingPolicyRepositoryTests;

public class ExistsPolicyTests
{
    private readonly InMemoryEmployeeBookingPolicyRepository _employeePolicyRepository;

    public ExistsPolicyTests()
    {
        _employeePolicyRepository = new InMemoryEmployeeBookingPolicyRepository();
    }

    [Fact]
    public void ExistsEmployeePolicy()
    {
        // Arrange
        _employeePolicyRepository.Add(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));

        // Act
        var exists = _employeePolicyRepository.Exists(1);

        // Assert
        exists.Should().BeTrue();
    }

    [Fact]
    public void DoesNotExistEmployeePolicy()
    {
        // Act
        var exists = _employeePolicyRepository.Exists(1);

        // Assert
        exists.Should().BeFalse();
    }
}