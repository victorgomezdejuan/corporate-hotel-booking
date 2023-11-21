using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Repositories.EmployeePolicies;

public interface IEmployeePolicyRepository
{
    void AddEmployeePolicy(EmployeePolicy employeePolicy);
    void UpdateEmployeePolicy(EmployeePolicy employeePolicy);
    bool Exists(int employeeId);
    EmployeePolicy GetEmployeePolicy(int employeeId);
}