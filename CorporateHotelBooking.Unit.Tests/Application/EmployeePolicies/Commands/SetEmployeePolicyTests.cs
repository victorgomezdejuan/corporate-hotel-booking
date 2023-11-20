using CorporateHotelBooking.Application.EmployeePolicies.Commands.SetEmployeePolicy;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.EmployeePolicies;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.EmployeePolicies.Commands;

public class SetEmployeePolicyTests
{
    private readonly Mock<IEmployeePolicyRepository> _employeePolicyRepositoryMock;
    private readonly SetEmployeePolicyCommandHandler _setEmployeePolicyCommandHandler;

    public SetEmployeePolicyTests()
    {
        _employeePolicyRepositoryMock = new Mock<IEmployeePolicyRepository>();
        _setEmployeePolicyCommandHandler = new SetEmployeePolicyCommandHandler(_employeePolicyRepositoryMock.Object);
    }

    [Fact]
    public void AddNewEmployeePolicy()
    {
        // Arrange
        var setEmployeePolicyCommand = new SetEmployeePolicyCommand(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        _setEmployeePolicyCommandHandler.Handle(setEmployeePolicyCommand);

        // Assert
        _employeePolicyRepositoryMock.Verify(r => r.AddEmployeePolicy(new EmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite })));
    }

    [Fact]
    public void UpdateExistingEmployeePolicy()
    {
        // Arrange
        _employeePolicyRepositoryMock.Setup(r => r.Exists(1)).Returns(true);
        var setEmployeePolicyCommand = new SetEmployeePolicyCommand(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        _setEmployeePolicyCommandHandler.Handle(setEmployeePolicyCommand);

        // Assert
        _employeePolicyRepositoryMock.Verify(r => r.UpdateEmployeePolicy(new EmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite })));
    }
}