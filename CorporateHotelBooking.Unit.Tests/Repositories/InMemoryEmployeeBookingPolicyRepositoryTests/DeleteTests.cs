using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeBookingPolicyRepositoryTests;

public class Delete
{
    [Fact]
    public void DeleteEmployeeBookingPolicy()
    {
        // Arrange
        var employeeBookingPolicyRepository = new InMemoryEmployeeBookingPolicyRepository();
        employeeBookingPolicyRepository.Add(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard }));

        // Act
        employeeBookingPolicyRepository.Delete(1);

        // Assert
        employeeBookingPolicyRepository.Exists(1).Should().BeFalse();
    }

    [Fact]
    public void DoNotDeleteOtherEmployeesPolicies()
    {
        // Arrange
        var employeeBookingPolicyRepository = new InMemoryEmployeeBookingPolicyRepository();
        employeeBookingPolicyRepository.Add(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard }));

        // Act
        employeeBookingPolicyRepository.Delete(2);

        // Assert
        employeeBookingPolicyRepository.Exists(1).Should().BeTrue();
    }
}