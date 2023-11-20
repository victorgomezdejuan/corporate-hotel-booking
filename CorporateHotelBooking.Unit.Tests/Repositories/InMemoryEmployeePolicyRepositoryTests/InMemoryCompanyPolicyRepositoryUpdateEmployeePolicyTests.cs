using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.EmployeePolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeePolicyRepositoryTests;

public class InMemoryCompanyPolicyRepositoryUpdateEmployeePolicyTests
{
    [Fact]
    public void UpdateEmployeePolicy()
    {
        // Arrange
        var employeePolicyRepository = new InMemoryEmployeePolicyRepository();
        employeePolicyRepository.AddEmployeePolicy(new EmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));
        var employeePolicytoBeUpdated = new EmployeePolicy(1, new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // Act
        employeePolicyRepository.UpdateEmployeePolicy(employeePolicytoBeUpdated);

        // Assert
        var retrievedEmployeePolicy = employeePolicyRepository.GetEmployeePolicy(1);
        retrievedEmployeePolicy.Should().Be(employeePolicytoBeUpdated);
    }
}