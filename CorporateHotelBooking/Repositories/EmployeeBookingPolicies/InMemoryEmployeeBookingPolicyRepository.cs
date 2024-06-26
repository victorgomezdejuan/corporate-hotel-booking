using CorporateHotelBooking.Domain.Entities.BookingPolicies;

namespace CorporateHotelBooking.Repositories.EmployeeBookingPolicies;

public class InMemoryEmployeeBookingPolicyRepository : IEmployeeBookingPolicyRepository
{
    private readonly Dictionary<int, EmployeeBookingPolicy> _employeePolicies = new();

    public void Add(EmployeeBookingPolicy employeePolicy)
    {
        _employeePolicies.Add(employeePolicy.EmployeeId, employeePolicy);
    }

    public void Update(EmployeeBookingPolicy employeePolicy)
    {
        _employeePolicies[employeePolicy.EmployeeId] = employeePolicy;
    }

    public void Delete(int employeeId)
    {
        _employeePolicies.Remove(employeeId);
    }

    public bool Exists(int employeeId)
    {
        return _employeePolicies.ContainsKey(employeeId);
    }

    public EmployeeBookingPolicy? Get(int employeeId)
    {
        if (!Exists(employeeId))
        {
            return null;
        }
        
        return _employeePolicies[employeeId];
    }
}