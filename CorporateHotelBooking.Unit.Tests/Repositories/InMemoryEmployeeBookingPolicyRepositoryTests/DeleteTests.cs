using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryEmployeeBookingPolicyRepositoryTests;

public class Delete
{
    private readonly InMemoryEmployeeBookingPolicyRepository _repository;

    public Delete()
    {
        _repository = new InMemoryEmployeeBookingPolicyRepository();
    }

    [Theory, AutoData]
    public void DeleteEmployeeBookingPolicy(int employeeId, [CollectionSize(1)] List<RoomType> allowedRoomTypes)
    {
        // Arrange
        _repository.Add(new EmployeeBookingPolicy(employeeId, allowedRoomTypes));

        // Act
        _repository.Delete(employeeId);

        // Assert
        _repository.Exists(employeeId).Should().BeFalse();
    }

    [Theory, AutoData]
    public void DoNotDeleteOtherEmployeesPolicies(int employeeId, [CollectionSize(1)] List<RoomType> allowedRoomTypes)
    {
        // Arrange
        _repository.Add(new EmployeeBookingPolicy(employeeId, allowedRoomTypes));
        var anotherEmployeeId = employeeId + 1;

        // Act
        _repository.Delete(anotherEmployeeId);

        // Assert
        _repository.Exists(employeeId).Should().BeTrue();
    }
}