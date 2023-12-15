using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeBookingPolicyRepositoryTests;

public class UpdateTests
{
    [Fact]
    public void UpdateEmployeePolicy()
    {
        // Arrange
        var repository = new InMemoryEmployeeBookingPolicyRepository();
        repository.Add(new EmployeeBookingPolicy(
            1,
            new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));
        var employeePolicytoBeUpdated = new EmployeeBookingPolicy(
            1,
            new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // Act
        repository.Update(employeePolicytoBeUpdated);

        // Assert
        var retrievedEmployeePolicy = repository.Get(1);
        retrievedEmployeePolicy.Should().Be(employeePolicytoBeUpdated);
    }
}