using AutoFixture.Xunit2;
using CorporateHotelBooking.Application.Employees.Commands.AddEmployee;
using CorporateHotelBooking.Domain.Entities;
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

    [Theory, AutoData]
    public void AddEmployee(int employeeId, int companyId)
    {
        // Arrange
        var addEmployeeCommand = new AddEmployeeCommand(employeeId, companyId);

        // Act
        var result = _addEmployeeCommandHandler.Handle(addEmployeeCommand);

        // Assert
        result.IsFailure.Should().BeFalse();
        _employeeRepository.Verify(r => r.Add(new Employee(employeeId, companyId)));
    }

    [Theory, AutoData]
    public void AddExistingEmployee(int employeeId, int companyId)
    {
        // Arrange
        _employeeRepository.Setup(r => r.Exists(employeeId)).Returns(true);

        // Act
        var result = _addEmployeeCommandHandler.Handle(new AddEmployeeCommand(employeeId, companyId));

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Employee already exists in the company");
    }
}