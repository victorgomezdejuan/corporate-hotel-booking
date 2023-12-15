using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeBookingPolicyRepositoryTests;

public class ExistsTests
{
    private readonly InMemoryEmployeeBookingPolicyRepository _repository;

    public ExistsTests()
    {
        _repository = new InMemoryEmployeeBookingPolicyRepository();
    }

    [Fact]
    public void ExistsEmployeePolicy()
    {
        // Arrange
        _repository.Add(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));

        // Act
        var exists = _repository.Exists(1);

        // Assert
        exists.Should().BeTrue();
    }

    [Fact]
    public void DoesNotExistEmployeePolicy()
    {
        // Act
        var exists = _repository.Exists(1);

        // Assert
        exists.Should().BeFalse();
    }
}