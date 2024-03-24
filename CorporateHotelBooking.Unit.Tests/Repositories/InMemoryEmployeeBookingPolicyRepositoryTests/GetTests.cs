using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeePolicyRepositoryTests;

public class GetTests
{
    private readonly InMemoryEmployeeBookingPolicyRepository _repository;

    public GetTests() => _repository = new InMemoryEmployeeBookingPolicyRepository();

    [Theory, AutoData]
    public void GetExistingEmployeePolicy(int employeeId, [CollectionSize(2)] List<RoomType> allowedRoomTypes)
    {
        // Arrange
        var employeeBookingPolicy = new EmployeeBookingPolicy(employeeId, allowedRoomTypes);
        _repository.Add(employeeBookingPolicy);

        // Act
        var retrievedEmployeePolicy = _repository.Get(employeeId);

        // Assert
        retrievedEmployeePolicy.Should().Be(employeeBookingPolicy);
    }

    [Theory, AutoData]
    public void GetNonExistingEmployeePolicy(int employeeId)
    {
        // Act
        var employee = _repository.Get(employeeId);

        // Assert
        employee.Should().BeNull();
    }
}