using CorporateHotelBooking.Application.BookingPolicies.Queries.IsBookingAllowed;
using CorporateHotelBooking.Application.CompanyPolicies.Commands.SetCompanyPolicy;
using CorporateHotelBooking.Application.EmployeePolicies.Commands.SetEmployeePolicy;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;
using CorporateHotelBooking.Repositories.EmployeePolicies;
using CorporateHotelBooking.Repositories.Employees;

namespace CorporateHotelBooking;

public class BookingPolicyService
{
    private ICompanyPolicyRepository _companyPolicyRepository;
    private IEmployeePolicyRepository _employeePolicyRepository;
    private readonly IEmployeeRepository _employeeRepository;
    
    public BookingPolicyService(
        ICompanyPolicyRepository companyPolicyRepository,
        IEmployeePolicyRepository employeePolicyRepository,
        IEmployeeRepository employeeRepository)
    {
        _companyPolicyRepository = companyPolicyRepository;
        _employeePolicyRepository = employeePolicyRepository;
        _employeeRepository = employeeRepository;
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
        return new IsBookingAllowedQueryHandler(_employeeRepository, _companyPolicyRepository, _employeePolicyRepository)
            .Handle(new IsBookingAllowedQuery(employeeId, roomType));
    }
}