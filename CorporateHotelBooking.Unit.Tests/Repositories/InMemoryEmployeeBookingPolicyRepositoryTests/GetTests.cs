using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeePolicyRepositoryTests;

public class GetTests
{
    private readonly InMemoryEmployeeBookingPolicyRepository _employeePolicyRepository;

    public GetTests()
    {
        _employeePolicyRepository = new InMemoryEmployeeBookingPolicyRepository();
    }

    [Fact]
    public void GetExistingEmployeePolicy()
    {
        // Arrange
        _employeePolicyRepository.Add(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));

        // Act
        var retrievedEmployeePolicy = _employeePolicyRepository.Get(1);

        // Assert
        retrievedEmployeePolicy.Should().Be(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));
    }

    [Fact]
    public void GetNonExistingEmployeePolicy()
    {
        // Act
        EmployeeBookingPolicy action() => _employeePolicyRepository.Get(1);

        // Assert
        Assert.Throws<EmployeeNotFoundException>(action);
    }
}