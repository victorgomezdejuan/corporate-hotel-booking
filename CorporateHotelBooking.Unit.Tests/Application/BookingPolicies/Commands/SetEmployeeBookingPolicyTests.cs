using CorporateHotelBooking.Application.BookingPolicies.Commands.SetEmployeeBookingPolicy;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.BookingPolicies.Commands;

public class SetEmployeeBookingPolicyTests
{
    private readonly Mock<IEmployeeBookingPolicyRepository> _employeePolicyRepositoryMock;
    private readonly SetEmployeeBookingPolicyCommandHandler _setEmployeePolicyCommandHandler;

    public SetEmployeeBookingPolicyTests()
    {
        _employeePolicyRepositoryMock = new Mock<IEmployeeBookingPolicyRepository>();
        _setEmployeePolicyCommandHandler = new SetEmployeeBookingPolicyCommandHandler(_employeePolicyRepositoryMock.Object);
    }

    [Fact]
    public void AddNewEmployeePolicy()
    {
        // Arrange
        var setEmployeePolicyCommand = new SetEmployeeBookingPolicyCommand(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        _setEmployeePolicyCommandHandler.Handle(setEmployeePolicyCommand);

        // Assert
        _employeePolicyRepositoryMock.Verify(r => r.AddEmployeePolicy(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite })));
    }

    [Fact]
    public void UpdateExistingEmployeePolicy()
    {
        // Arrange
        _employeePolicyRepositoryMock.Setup(r => r.Exists(1)).Returns(true);
        var setEmployeePolicyCommand = new SetEmployeeBookingPolicyCommand(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        _setEmployeePolicyCommandHandler.Handle(setEmployeePolicyCommand);

        // Assert
        _employeePolicyRepositoryMock.Verify(r => r.UpdateEmployeePolicy(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite })));
    }
}