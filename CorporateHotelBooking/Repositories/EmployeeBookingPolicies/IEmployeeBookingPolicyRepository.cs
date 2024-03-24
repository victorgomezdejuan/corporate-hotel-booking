using CorporateHotelBooking.Domain.Entities.BookingPolicies;

namespace CorporateHotelBooking.Repositories.EmployeeBookingPolicies;

public interface IEmployeeBookingPolicyRepository
{
    void Add(EmployeeBookingPolicy employeePolicy);
    void Update(EmployeeBookingPolicy employeePolicy);
    void Delete(int employeeId);
    bool Exists(int employeeId);
    EmployeeBookingPolicy? Get(int employeeId);
}