using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeePolicyRepositoryTests;

public class GetTests
{
    private readonly InMemoryEmployeeBookingPolicyRepository _repository;

    public GetTests()
    {
        _repository = new InMemoryEmployeeBookingPolicyRepository();
    }

    [Fact]
    public void GetExistingEmployeePolicy()
    {
        // Arrange
        _repository.Add(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));

        // Act
        var retrievedEmployeePolicy = _repository.Get(1);

        // Assert
        retrievedEmployeePolicy
            .Should().Be(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));
    }

    [Fact]
    public void GetNonExistingEmployeePolicy()
    {
        // Act
        EmployeeBookingPolicy action() => _repository.Get(1);

        // Assert
        Assert.Throws<EmployeeNotFoundException>(action);
    }
}