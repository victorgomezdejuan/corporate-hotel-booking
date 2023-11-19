using CorporateHotelBooking.Application.EmployeePolicies.Commands.SetEmployeePolicy;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.EmployeePolicies;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.EmployeePolicies.Commands;

public class SetEmployeePolicyTests
{
    [Fact]
    public void AddNewEmployeePolicy()
    {
        // Arrange
        var employeePolicyRepositoryMock = new Mock<IEmployeePolicyRepository>();
        var setEmployeePolicyCommandHandler = new SetEmployeePolicyCommandHandler(employeePolicyRepositoryMock.Object);
        var setEmployeePolicyCommand = new SetEmployeePolicyCommand(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        setEmployeePolicyCommandHandler.Handle(setEmployeePolicyCommand);

        // Assert
        employeePolicyRepositoryMock.Verify(r => r.AddEmployeePolicy(new EmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite })));
    }
}