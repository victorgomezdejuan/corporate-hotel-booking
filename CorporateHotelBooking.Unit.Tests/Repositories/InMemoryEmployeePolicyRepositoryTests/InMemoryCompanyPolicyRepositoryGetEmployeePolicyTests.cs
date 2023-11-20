using CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.EmployeePolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeePolicyRepositoryTests;

public class InMemoryCompanyPolicyRepositoryGetEmployeePolicyTests
{
    private readonly InMemoryEmployeePolicyRepository _employeePolicyRepository;

    public InMemoryCompanyPolicyRepositoryGetEmployeePolicyTests()
    {
        _employeePolicyRepository = new InMemoryEmployeePolicyRepository();
    }

    [Fact]
    public void GetExistingEmployeePolicy()
    {
        // Arrange
        _employeePolicyRepository.AddEmployeePolicy(new EmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));

        // Act
        var retrievedEmployeePolicy = _employeePolicyRepository.GetEmployeePolicy(1);

        // Assert
        retrievedEmployeePolicy.Should().Be(new EmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));
    }

    [Fact]
    public void GetNonExistingEmployeePolicy()
    {
        // Act
        EmployeePolicy action() => _employeePolicyRepository.GetEmployeePolicy(1);

        // Assert
        Assert.Throws<EmployeeNotFoundException>(action);
    }
}