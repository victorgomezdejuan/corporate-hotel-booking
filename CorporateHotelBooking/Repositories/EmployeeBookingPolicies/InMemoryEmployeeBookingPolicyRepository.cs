using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;

namespace CorporateHotelBooking.Repositories.EmployeeBookingPolicies;

public class InMemoryEmployeeBookingPolicyRepository : IEmployeeBookingPolicyRepository
{
    private readonly Dictionary<int, EmployeeBookingPolicy> _employeePolicies = new();

    public void AddEmployeePolicy(EmployeeBookingPolicy employeePolicy)
    {
        _employeePolicies.Add(employeePolicy.EmployeeId, employeePolicy);
    }

    public bool Exists(int employeeId)
    {
        return _employeePolicies.ContainsKey(employeeId);
    }

    public EmployeeBookingPolicy GetEmployeePolicy(int employeeId)
    {
        if (!Exists(employeeId))
        {
            throw new EmployeeNotFoundException(employeeId);
        }
        return _employeePolicies[employeeId];
    }

    public void UpdateEmployeePolicy(EmployeeBookingPolicy employeePolicy)
    {
        _employeePolicies[employeePolicy.EmployeeId] = employeePolicy;
    }
}