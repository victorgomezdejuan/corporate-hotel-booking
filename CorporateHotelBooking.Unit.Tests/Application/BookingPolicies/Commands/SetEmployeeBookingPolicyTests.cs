using AutoFixture.Xunit2;
using CorporateHotelBooking.Application.BookingPolicies.Commands.SetEmployeeBookingPolicy;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;
using FluentAssertions;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.BookingPolicies.Commands;

public class SetEmployeeBookingPolicyTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly Mock<IEmployeeBookingPolicyRepository> _employeePolicyRepositoryMock;
    private readonly SetEmployeeBookingPolicyCommandHandler _setEmployeePolicyCommandHandler;

    public SetEmployeeBookingPolicyTests()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        _employeePolicyRepositoryMock = new Mock<IEmployeeBookingPolicyRepository>();
        _setEmployeePolicyCommandHandler =
            new SetEmployeeBookingPolicyCommandHandler(
                _employeeRepositoryMock.Object,
                _employeePolicyRepositoryMock.Object);
    }

    [Theory, AutoData]
    public void AddNewEmployeePolicy(int employeeId, [CollectionSize(2)] List<RoomType> roomTypes)
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(employeeId)).Returns(true);

        var setEmployeePolicyCommand = new SetEmployeeBookingPolicyCommand(employeeId, roomTypes);

        // Act
        _setEmployeePolicyCommandHandler.Handle(setEmployeePolicyCommand);

        // Assert
        _employeePolicyRepositoryMock.Verify(r => r.Add(new EmployeeBookingPolicy(employeeId, roomTypes)));
    }

    [Theory, AutoData]
    public void UpdateExistingEmployeePolicy(int employeeId, [CollectionSize(2)] List<RoomType> roomTypes)
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(employeeId)).Returns(true);
        _employeePolicyRepositoryMock.Setup(r => r.Exists(employeeId)).Returns(true);

        var setEmployeePolicyCommand = new SetEmployeeBookingPolicyCommand(employeeId, roomTypes);

        // Act
        _setEmployeePolicyCommandHandler.Handle(setEmployeePolicyCommand);

        // Assert
        _employeePolicyRepositoryMock.Verify(r => r.Update(new EmployeeBookingPolicy(employeeId, roomTypes)));
    }

    [Theory, AutoData]
    public void NonExistingEmployee(int employeeId, [CollectionSize(2)] List<RoomType> roomTypes)
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(employeeId)).Returns(false);

        var setEmployeePolicyCommand = new SetEmployeeBookingPolicyCommand(employeeId, roomTypes);

        // Act
        var result = _setEmployeePolicyCommandHandler.Handle(setEmployeePolicyCommand);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Employee not found");
    }
}