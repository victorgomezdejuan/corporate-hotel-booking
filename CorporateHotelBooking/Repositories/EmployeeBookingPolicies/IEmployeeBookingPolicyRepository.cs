using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Repositories.EmployeeBookingPolicies;

public interface IEmployeeBookingPolicyRepository
{
    void AddEmployeePolicy(EmployeeBookingPolicy employeePolicy);
    void UpdateEmployeePolicy(EmployeeBookingPolicy employeePolicy);
    bool Exists(int employeeId);
    EmployeeBookingPolicy GetEmployeePolicy(int employeeId);
}