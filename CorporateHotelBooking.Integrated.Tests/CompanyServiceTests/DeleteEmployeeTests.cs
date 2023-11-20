using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.CompanyServiceTests;

public class DeleteEmployeeTests
{
    [Fact]
    public void DeleteEmployee()
    {
        // Arrange
        IEmployeeRepository employeeRepository = new InMemoryEmployeeRepository();
        var companyService = new CompanyService(employeeRepository);
        companyService.AddEmployee(companyId: 100, employeeId: 1);

        // Act
        companyService.DeleteEmployee(1);

        // Assert
        Action action = () => employeeRepository.GetEmployee(1);
        action.Should().Throw<EmployeeNotFoundException>();
        // TODO: When deleting an employee, all the bookings and policies associated to the employee should also be deleted from the system.
        // So far we cannot do it because we haven't implemented the Booking Policy Service and Booking Service.
    }
}