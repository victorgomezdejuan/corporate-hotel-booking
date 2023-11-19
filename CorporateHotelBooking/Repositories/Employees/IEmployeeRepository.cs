using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Repositories.Employees;

public interface IEmployeeRepository
{
    void AddEmployee(Employee employee);
    void DeleteEmployee(int employeeId);
    bool Exists(int employeeId);
    Employee GetEmployee(int employeeId);
}