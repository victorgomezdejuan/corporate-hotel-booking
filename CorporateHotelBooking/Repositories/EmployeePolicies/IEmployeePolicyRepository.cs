using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Repositories.EmployeePolicies;

public interface IEmployeePolicyRepository
{
    void AddEmployeePolicy(EmployeePolicy employeePolicy);
}