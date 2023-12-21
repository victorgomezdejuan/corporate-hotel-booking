using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeBookingPolicyRepositoryTests;

public class AddTests
{
    [Theory, AutoData]
    public void AddEmployeePolicy(int employeeId, [CollectionSize(2)] List<RoomType> allowedRoomTypes)
    {
        // Arrange
        var employeePolicyRepository = new InMemoryEmployeeBookingPolicyRepository();
        var employeePolicyToBeAdded = new EmployeeBookingPolicy(employeeId, allowedRoomTypes);

        // Act
        employeePolicyRepository.Add(employeePolicyToBeAdded);

        // Assert
        var retrievedEmployeePolicy = employeePolicyRepository.Get(employeeId);
        retrievedEmployeePolicy.Should().Be(employeePolicyToBeAdded);
    }
}