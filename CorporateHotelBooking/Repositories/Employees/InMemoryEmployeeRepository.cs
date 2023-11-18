using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Repositories.Employees;

public class InMemoryEmployeeRepository : IEmployeeRepository
{
    private readonly Dictionary<int, Employee> _employees = new();

    public void AddEmployee(Employee employee)
    {
        _employees.Add(employee.Id, employee);
    }

    public Employee GetEmployee(int employeeId)
    {
        return _employees[employeeId];
    }
}