using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Repositories.Employees;

public class InMemoryEmployeeRepository : IEmployeeRepository
{
    private readonly Dictionary<int, Employee> _employees = new();

    public void Add(Employee employee)
    {
        _employees.Add(employee.Id, employee);
    }

    public void Delete(int employeeId)
    {
        if (_employees.ContainsKey(employeeId))
        {
            _employees.Remove(employeeId);
        }
    }

    public bool Exists(int employeeId)
    {
        return _employees.ContainsKey(employeeId);
    }

    public Employee? Get(int employeeId)
    {
        if (!_employees.ContainsKey(employeeId))
        {
            return null;
        }
        return _employees[employeeId];
    }
}