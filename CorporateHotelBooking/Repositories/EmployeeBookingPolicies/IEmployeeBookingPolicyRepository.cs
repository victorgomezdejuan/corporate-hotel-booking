using CorporateHotelBooking.Domain.Entities.BookingPolicies;

namespace CorporateHotelBooking.Repositories.EmployeeBookingPolicies;

public interface IEmployeeBookingPolicyRepository
{
    void Add(EmployeeBookingPolicy employeePolicy);
    void UpdateEmployeePolicy(EmployeeBookingPolicy employeePolicy);
    bool Exists(int employeeId);
    EmployeeBookingPolicy GetEmployeePolicy(int employeeId);
}