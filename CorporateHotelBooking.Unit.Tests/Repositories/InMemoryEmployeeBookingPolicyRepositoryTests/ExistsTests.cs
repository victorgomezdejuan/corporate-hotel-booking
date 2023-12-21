using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeBookingPolicyRepositoryTests;

public class ExistsTests
{
    private readonly InMemoryEmployeeBookingPolicyRepository _repository;

    public ExistsTests()
    {
        _repository = new InMemoryEmployeeBookingPolicyRepository();
    }

    [Theory, AutoData]
    public void ExistsEmployeePolicy(int employeeId, [CollectionSize(2)] List<RoomType> allowedRoomTypes)
    {
        // Arrange
        _repository.Add(new EmployeeBookingPolicy(employeeId, allowedRoomTypes));

        // Act
        var exists = _repository.Exists(employeeId);

        // Assert
        exists.Should().BeTrue();
    }

    [Theory, AutoData]
    public void DoesNotExistEmployeePolicy(int employeeId)
    {
        // Act
        var exists = _repository.Exists(employeeId);

        // Assert
        exists.Should().BeFalse();
    }
}