using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeBookingPolicyRepositoryTests;

public class InMemoryEmployeeBookingPolicyRepositoryAddEmployeePolicyTests
{
    [Fact]
    public void AddEmployeePolicy()
    {
        // Arrange
        var employeePolicyRepository = new InMemoryEmployeeBookingPolicyRepository();
        var employeePolicyToBeAdded = new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        employeePolicyRepository.AddEmployeePolicy(employeePolicyToBeAdded);

        // Assert
        var retrievedEmployeePolicy = employeePolicyRepository.GetEmployeePolicy(1);
        retrievedEmployeePolicy.Should().Be(employeePolicyToBeAdded);
    }
}