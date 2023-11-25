using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeBookingPolicyRepositoryTests;

public class InMemoryEmployeeBookingPolicyRepositoryExistsPolicyTests
{
    private readonly InMemoryEmployeeBookingPolicyRepository _employeePolicyRepository;

    public InMemoryEmployeeBookingPolicyRepositoryExistsPolicyTests()
    {
        _employeePolicyRepository = new InMemoryEmployeeBookingPolicyRepository();
    }

    [Fact]
    public void ExistsEmployeePolicy()
    {
        // Arrange
        _employeePolicyRepository.AddEmployeePolicy(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));

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