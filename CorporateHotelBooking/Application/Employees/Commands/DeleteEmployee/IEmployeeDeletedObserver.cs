namespace CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;

public interface IEmployeeDeletedObserver
{
    void Notify(int employeeId);
}