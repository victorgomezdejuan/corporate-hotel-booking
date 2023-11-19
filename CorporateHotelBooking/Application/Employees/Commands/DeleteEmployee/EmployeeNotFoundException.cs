namespace CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;

public class EmployeeNotFoundException : Exception
{
    public EmployeeNotFoundException(int employeeId) : base($"Employee with id {employeeId} was not found") { }
}