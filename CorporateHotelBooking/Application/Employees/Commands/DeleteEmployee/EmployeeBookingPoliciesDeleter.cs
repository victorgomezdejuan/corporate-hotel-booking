using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;

namespace CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;

public class EmployeeBookingPoliciesDeleter : IEmployeeDeletedObserver
{
    private readonly IEmployeeBookingPolicyRepository _employeeBookingPolicyRepository;

    public EmployeeBookingPoliciesDeleter(
        IEmployeeBookingPolicyRepository employeeBookingPolicyRepository)
    {
        _employeeBookingPolicyRepository = employeeBookingPolicyRepository;
    }

    public void Notify(int employeeId)
    {
        _employeeBookingPolicyRepository.Delete(employeeId);
    }
}