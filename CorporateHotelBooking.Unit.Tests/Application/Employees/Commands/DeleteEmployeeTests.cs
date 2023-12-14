using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.Employees.Commands;

public class DeleteEmployeeTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly Mock<IBookingRepository> _bookingRepositoryMock;
    private readonly DeleteEmployeeCommandHandler _deleteEmployeeCommandHandler;

    public DeleteEmployeeTests()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        _bookingRepositoryMock = new Mock<IBookingRepository>();
        _deleteEmployeeCommandHandler = new DeleteEmployeeCommandHandler(_employeeRepositoryMock.Object, _bookingRepositoryMock.Object);
    }

    [Fact]
    public void DeleteExistingEmployee()
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(1)).Returns(true);

        // Act
        _deleteEmployeeCommandHandler.Handle(new DeleteEmployeeCommand(1));

        // Assert
        _employeeRepositoryMock.Verify(r => r.Delete(1));
    }

    [Fact]
    public void DeleteNonExistingEmployee()
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(1)).Returns(false);

        // Act
        Action action = () => _deleteEmployeeCommandHandler.Handle(new DeleteEmployeeCommand(1));

        // Assert
        action.Should().Throw<EmployeeNotFoundException>();
    }

    [Fact]
    public void DeleteEmployeeBookingWhenDeletingEmployee()
    {
        // Arrange
        _employeeRepositoryMock.Setup(r => r.Exists(1)).Returns(true);

        // Act
        _deleteEmployeeCommandHandler.Handle(new DeleteEmployeeCommand(1));

        // Assert
        _employeeRepositoryMock.Verify(r => r.Delete(1));
        _bookingRepositoryMock.Verify(r => r.DeleteByEmployeeId(1));
    }
}