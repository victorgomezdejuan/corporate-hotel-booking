using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeBookingPolicyRepositoryTests;

public class InMemoryEmployeePolicyRepositoryUpdateEmployeePolicyTests
{
    [Fact]
    public void UpdateEmployeePolicy()
    {
        // Arrange
        var employeePolicyRepository = new InMemoryEmployeeBookingPolicyRepository();
        employeePolicyRepository.AddEmployeePolicy(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));
        var employeePolicytoBeUpdated = new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // Act
        employeePolicyRepository.UpdateEmployeePolicy(employeePolicytoBeUpdated);

        // Assert
        var retrievedEmployeePolicy = employeePolicyRepository.GetEmployeePolicy(1);
        retrievedEmployeePolicy.Should().Be(employeePolicytoBeUpdated);
    }
}