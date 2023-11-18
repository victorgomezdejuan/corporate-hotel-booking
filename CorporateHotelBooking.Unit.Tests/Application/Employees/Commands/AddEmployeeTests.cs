using CorporateHotelBooking.Application.Employees;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Employees;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.Employees.Commands;

public class AddEmployeeTests
{
    [Fact]
    public void AddEmployee()
    {
        // Arrange
        var employeeRepository = new Mock<IEmployeeRepository>();
        var addEmployeeCommand = new AddEmployeeCommand(1, 100);
        var addEmployeeCommandHandler = new AddEmployeeCommandHandler(employeeRepository.Object);

        // Act
        addEmployeeCommandHandler.Handle(addEmployeeCommand);

        // Assert
        employeeRepository.Verify(r => r.AddEmployee(new Employee(1, 100)));
    }
}