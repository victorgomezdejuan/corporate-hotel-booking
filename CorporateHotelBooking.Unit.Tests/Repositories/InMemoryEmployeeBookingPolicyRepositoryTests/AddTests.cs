using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeBookingPolicyRepositoryTests;

public class AddTests
{
    [Fact]
    public void AddEmployeePolicy()
    {
        // Arrange
        var employeePolicyRepository = new InMemoryEmployeeBookingPolicyRepository();
        var employeePolicyToBeAdded = new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        employeePolicyRepository.Add(employeePolicyToBeAdded);

        // Assert
        var retrievedEmployeePolicy = employeePolicyRepository.Get(1);
        retrievedEmployeePolicy.Should().Be(employeePolicyToBeAdded);
    }
}