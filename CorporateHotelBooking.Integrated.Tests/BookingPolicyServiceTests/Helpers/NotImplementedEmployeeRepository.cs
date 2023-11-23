using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Employees;

namespace CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests.Helpers;

public class NotImplementedEmployeeRepository: IEmployeeRepository
{
    public void AddEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }

    public void DeleteEmployee(int employeeId)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int employeeId)
    {
        throw new NotImplementedException();
    }

    public Employee GetEmployee(int employeeId)
    {
        throw new NotImplementedException();
    }
}