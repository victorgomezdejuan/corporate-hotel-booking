using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeePolicyRepositoryTests;

public class InMemoryEmployeeBookingPolicyRepositoryGetEmployeePolicyTests
{
    private readonly InMemoryEmployeeBookingPolicyRepository _employeePolicyRepository;

    public InMemoryEmployeeBookingPolicyRepositoryGetEmployeePolicyTests()
    {
        _employeePolicyRepository = new InMemoryEmployeeBookingPolicyRepository();
    }

    [Fact]
    public void GetExistingEmployeePolicy()
    {
        // Arrange
        _employeePolicyRepository.AddEmployeePolicy(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));

        // Act
        var retrievedEmployeePolicy = _employeePolicyRepository.GetEmployeePolicy(1);

        // Assert
        retrievedEmployeePolicy.Should().Be(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));
    }

    [Fact]
    public void GetNonExistingEmployeePolicy()
    {
        // Act
        EmployeeBookingPolicy action() => _employeePolicyRepository.GetEmployeePolicy(1);

        // Assert
        Assert.Throws<EmployeeNotFoundException>(action);
    }
}