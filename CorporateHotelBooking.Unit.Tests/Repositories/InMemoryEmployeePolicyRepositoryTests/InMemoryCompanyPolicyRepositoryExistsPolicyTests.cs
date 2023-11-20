using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.EmployeePolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeePolicyRepositoryTests;

public class InMemoryCompanyPolicyRepositoryExistsPolicyTests
{
    [Fact]
    public void ExistsEmployeePolicy()
    {
        // Arrange
        var employeePolicyRepository = new InMemoryEmployeePolicyRepository();
        employeePolicyRepository.AddEmployeePolicy(new EmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));

        // Act
        var exists = employeePolicyRepository.Exists(1);

        // Assert
        exists.Should().BeTrue();
    }

    [Fact]
    public void DoesNotExistEmployeePolicy()
    {
        // Arrange
        var employeePolicyRepository = new InMemoryEmployeePolicyRepository();

        // Act
        var exists = employeePolicyRepository.Exists(1);

        // Assert
        exists.Should().BeFalse();
    }
}