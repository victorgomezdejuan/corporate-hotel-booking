using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.EmployeePolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeePolicyRepositoryTests;

public class InMemoryCompanyPolicyRepositoryExistsPolicyTests
{
    private readonly InMemoryEmployeePolicyRepository _employeePolicyRepository;

    public InMemoryCompanyPolicyRepositoryExistsPolicyTests()
    {
        _employeePolicyRepository = new InMemoryEmployeePolicyRepository();
    }

    [Fact]
    public void ExistsEmployeePolicy()
    {
        // Arrange
        _employeePolicyRepository.AddEmployeePolicy(new EmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));

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