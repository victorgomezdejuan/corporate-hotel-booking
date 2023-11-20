using CorporateHotelBooking.Application.CompanyPolicies.Commands.SetCompanyPolicy;
using CorporateHotelBooking.Application.EmployeePolicies.Commands.SetEmployeePolicy;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;
using CorporateHotelBooking.Repositories.EmployeePolicies;

namespace CorporateHotelBooking;

public class BookingPolicyService
{
    private ICompanyPolicyRepository _companyPolicyRepository;
    private IEmployeePolicyRepository _employeePolicyRepository;
    
    public BookingPolicyService(ICompanyPolicyRepository companyPolicyRepository, IEmployeePolicyRepository employeePolicyRepository)
    {
        _companyPolicyRepository = companyPolicyRepository;
        _employeePolicyRepository = employeePolicyRepository;
    }

    public void SetCompanyPolicy(int companyId, ICollection<RoomType> roomTypes)
    {
        new SetCompanyPolicyCommandHandler(_companyPolicyRepository).Handle(new SetCompanyPolicyCommand(companyId, roomTypes));
    }

    public void SetEmployeePolicy(int employeeId, ICollection<RoomType> roomTypes)
    {
        new SetEmployeePolicyCommandHandler(_employeePolicyRepository).Handle(new SetEmployeePolicyCommand(employeeId, roomTypes));
    }

    public bool IsBookingAllowed(int employeeId, RoomType roomType)
    {
        throw new NotImplementedException();
    }
}