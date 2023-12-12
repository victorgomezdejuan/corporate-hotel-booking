using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;

namespace CorporateHotelBooking.Repositories.EmployeeBookingPolicies;

public class InMemoryEmployeeBookingPolicyRepository : IEmployeeBookingPolicyRepository
{
    private readonly Dictionary<int, EmployeeBookingPolicy> _employeePolicies = new();

    public void Add(EmployeeBookingPolicy employeePolicy)
    {
        _employeePolicies.Add(employeePolicy.EmployeeId, employeePolicy);
    }

    public bool Exists(int employeeId)
    {
        return _employeePolicies.ContainsKey(employeeId);
    }

    public EmployeeBookingPolicy Get(int employeeId)
    {
        if (!Exists(employeeId))
        {
            throw new EmployeeNotFoundException(employeeId);
        }
        return _employeePolicies[employeeId];
    }

    public void Update(EmployeeBookingPolicy employeePolicy)
    {
        _employeePolicies[employeePolicy.EmployeeId] = employeePolicy;
    }
}