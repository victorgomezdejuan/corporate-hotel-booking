using AutoFixture.Xunit2;
using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.Employees.Commands;

public class DeleteEmployeeTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly DeleteEmployeeCommandHandler _deleteEmployeeCommandHandler;

    public DeleteEmployeeTests()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        _deleteEmployeeCommandHandler = new DeleteEmployeeCommandHandler(
            _employeeRepositoryMock.Object);
    }

    [Theory, AutoData]
    public void DeleteExistingEmployee(int employeeId)
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(employeeId)).Returns(true);

        // Act
        _deleteEmployeeCommandHandler.Handle(new DeleteEmployeeCommand(employeeId));

        // Assert
        _employeeRepositoryMock.Verify(r => r.Delete(employeeId));
    }

    [Theory, AutoData]
    public void DeleteNonExistingEmployee(int employeeId)
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(employeeId)).Returns(false);

        // Act
        Action action = () => _deleteEmployeeCommandHandler.Handle(new DeleteEmployeeCommand(employeeId));

        // Assert
        action.Should().Throw<EmployeeNotFoundException>();
    }

    [Theory, AutoData]
    public void NotifyWhenAnEmployeeHasBeenDeleted(int employeeId)
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(employeeId)).Returns(true);
        var suscriberMock = new Mock<IEmployeeDeletedObserver>();
        _deleteEmployeeCommandHandler.Subscribe(suscriberMock.Object);

        // Act
        _deleteEmployeeCommandHandler.Handle(new DeleteEmployeeCommand(employeeId));

        // Assert
        suscriberMock.Verify(s => s.Notify(employeeId));
    }
}