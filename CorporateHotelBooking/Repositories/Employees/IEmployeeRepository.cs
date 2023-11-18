using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Repositories.Employees;

public interface IEmployeeRepository
{
    void AddEmployee(Employee employee);
    Employee GetEmployee(int employeeId);
}