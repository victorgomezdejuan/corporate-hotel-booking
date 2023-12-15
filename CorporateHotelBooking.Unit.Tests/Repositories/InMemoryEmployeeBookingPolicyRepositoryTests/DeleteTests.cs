using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeBookingPolicyRepositoryTests;

public class Delete
{
    private readonly InMemoryEmployeeBookingPolicyRepository _repository;

    public Delete()
    {
        _repository = new InMemoryEmployeeBookingPolicyRepository();
    }

    [Fact]
    public void DeleteEmployeeBookingPolicy()
    {
        // Arrange
        _repository.Add(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard }));

        // Act
        _repository.Delete(1);

        // Assert
        _repository.Exists(1).Should().BeFalse();
    }

    [Fact]
    public void DoNotDeleteOtherEmployeesPolicies()
    {
        // Arrange
        _repository.Add(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard }));

        // Act
        _repository.Delete(2);

        // Assert
        _repository.Exists(1).Should().BeTrue();
    }
}