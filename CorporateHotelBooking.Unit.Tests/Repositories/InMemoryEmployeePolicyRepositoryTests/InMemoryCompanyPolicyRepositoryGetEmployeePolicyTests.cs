using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.EmployeePolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeePolicyRepositoryTests;

public class InMemoryCompanyPolicyRepositoryGetEmployeePolicyTests
{
    [Fact]
    public void GetExistingEmployeePolicy()
    {
        // Arrange
        var employeePolicyRepository = new InMemoryEmployeePolicyRepository();
        employeePolicyRepository.AddEmployeePolicy(new EmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));

        // Act
        var retrievedEmployeePolicy = employeePolicyRepository.GetEmployeePolicy(1);

        // Assert
        retrievedEmployeePolicy.Should().Be(new EmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));
    }
}