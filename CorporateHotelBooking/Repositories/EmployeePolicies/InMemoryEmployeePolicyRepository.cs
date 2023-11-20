using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Repositories.EmployeePolicies;

public class InMemoryEmployeePolicyRepository : IEmployeePolicyRepository
{
    private readonly Dictionary<int, EmployeePolicy> _employeePolicies = new();

    public void AddEmployeePolicy(EmployeePolicy employeePolicy)
    {
        _employeePolicies.Add(employeePolicy.EmployeeId, employeePolicy);
    }

    public bool Exists(int employeeId)
    {
        return _employeePolicies.ContainsKey(employeeId);
    }

    public EmployeePolicy GetEmployeePolicy(int employeeId)
    {
        if (!Exists(employeeId))
        {
            throw new EmployeeNotFoundException(employeeId);
        }
        return _employeePolicies[employeeId];
    }

    public void UpdateEmployeePolicy(EmployeePolicy employeePolicy)
    {
        _employeePolicies[employeePolicy.EmployeeId] = employeePolicy;
    }
}