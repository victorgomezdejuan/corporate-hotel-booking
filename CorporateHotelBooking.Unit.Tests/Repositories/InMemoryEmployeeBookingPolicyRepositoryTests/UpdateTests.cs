using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeBookingPolicyRepositoryTests;

public class UpdateTests
{
    [Theory, AutoData]
    public void UpdateEmployeePolicy(int employeeId)
    {
        // Arrange
        var repository = new InMemoryEmployeeBookingPolicyRepository();
        repository.Add(new EmployeeBookingPolicy(
            employeeId,
            new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));
        var employeePolicytoBeUpdated = new EmployeeBookingPolicy(
            employeeId,
            new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // Act
        repository.Update(employeePolicytoBeUpdated);

        // Assert
        var retrievedEmployeePolicy = repository.Get(employeeId);
        retrievedEmployeePolicy.Should().Be(employeePolicytoBeUpdated);
    }
}