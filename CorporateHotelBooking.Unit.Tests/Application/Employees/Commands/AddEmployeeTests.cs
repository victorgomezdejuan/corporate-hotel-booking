using CorporateHotelBooking.Application.Employees.Commands;
using CorporateHotelBooking.Application.Employees.Commands.AddEmployee;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.Employees.Commands;

public class AddEmployeeTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepository;
    private readonly AddEmployeeCommandHandler _addEmployeeCommandHandler;

    public AddEmployeeTests()
    {
        _employeeRepository = new Mock<IEmployeeRepository>();
        _addEmployeeCommandHandler = new AddEmployeeCommandHandler(_employeeRepository.Object);
    }

    [Fact]
    public void AddEmployee()
    {
        // Arrange
        var addEmployeeCommand = new AddEmployeeCommand(1, 100);

        // Act
        _addEmployeeCommandHandler.Handle(addEmployeeCommand);

        // Assert
        _employeeRepository.Verify(r => r.AddEmployee(new Employee(1, 100)));
    }

    [Fact]
    public void AddExistingEmployee()
    {
        // Arrange
        _employeeRepository.Setup(r => r.Exists(1)).Returns(true);

        // Act
        Action action = () => _addEmployeeCommandHandler.Handle(new AddEmployeeCommand(1, 100));

        // Assert
        action.Should().Throw<EmployeeAlreadyExistsException>();
    }
}