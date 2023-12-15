using CorporateHotelBooking.Application.BookingPolicies.Queries.IsBookingAllowed;
using CorporateHotelBooking.Application.BookingPolicies.Commands.SetCompanyBookingPolicy;
using CorporateHotelBooking.Application.BookingPolicies.Commands.SetEmployeeBookingPolicy;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;

namespace CorporateHotelBooking.Services;

public class BookingPolicyService
{
    private readonly ICompanyBookingPolicyRepository _companyPolicyRepository;
    private readonly IEmployeeBookingPolicyRepository _employeePolicyRepository;
    private readonly IEmployeeRepository _employeeRepository;
    
    public BookingPolicyService(
        ICompanyBookingPolicyRepository companyPolicyRepository,
        IEmployeeBookingPolicyRepository employeePolicyRepository,
        IEmployeeRepository employeeRepository)
    {
        _companyPolicyRepository = companyPolicyRepository;
        _employeePolicyRepository = employeePolicyRepository;
        _employeeRepository = employeeRepository;
    }

    public void SetCompanyPolicy(int companyId, ICollection<RoomType> roomTypes)
    {
        new SetCompanyBookingPolicyCommandHandler(_companyPolicyRepository).Handle(new SetCompanyBookingPolicyCommand(companyId, roomTypes));
    }

    public void SetEmployeePolicy(int employeeId, ICollection<RoomType> roomTypes)
    {
        new SetEmployeeBookingPolicyCommandHandler(_employeePolicyRepository).Handle(new SetEmployeeBookingPolicyCommand(employeeId, roomTypes));
    }

    public bool IsBookingAllowed(int employeeId, RoomType roomType)
    {
        return new IsBookingAllowedQueryHandler(_employeeRepository, _companyPolicyRepository, _employeePolicyRepository)
            .Handle(new IsBookingAllowedQuery(employeeId, roomType));
    }
}