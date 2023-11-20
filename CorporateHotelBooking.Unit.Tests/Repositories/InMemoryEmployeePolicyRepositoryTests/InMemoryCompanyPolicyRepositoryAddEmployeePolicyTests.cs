using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.EmployeePolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeePolicyRepositoryTests;

public class InMemoryCompanyPolicyRepositoryAddEmployeePolicyTests
{
    [Fact]
    public void AddEmployeePolicy()
    {
        // Arrange
        var employeePolicyRepository = new InMemoryEmployeePolicyRepository();
        var employeePolicyToBeAdded = new EmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        employeePolicyRepository.AddEmployeePolicy(employeePolicyToBeAdded);

        // Assert
        var retrievedEmployeePolicy = employeePolicyRepository.GetEmployeePolicy(1);
        retrievedEmployeePolicy.Should().Be(employeePolicyToBeAdded);
    }
}