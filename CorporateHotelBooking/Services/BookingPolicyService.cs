using CorporateHotelBooking.Application.BookingPolicies.Queries.IsBookingAllowed;
using CorporateHotelBooking.Application.BookingPolicies.Commands.SetCompanyBookingPolicy;
using CorporateHotelBooking.Application.BookingPolicies.Commands.SetEmployeeBookingPolicy;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using CorporateHotelBooking.Application.Common;

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

    public Result SetCompanyPolicy(int companyId, ICollection<RoomType> roomTypes)
    {
        return new SetCompanyBookingPolicyCommandHandler(_companyPolicyRepository)
            .Handle(new SetCompanyBookingPolicyCommand(companyId, roomTypes));
    }

    public void SetEmployeePolicy(int employeeId, ICollection<RoomType> roomTypes)
    {
        new SetEmployeeBookingPolicyCommandHandler(_employeePolicyRepository)
            .Handle(new SetEmployeeBookingPolicyCommand(employeeId, roomTypes));
    }

    public Result<bool> IsBookingAllowed(int employeeId, RoomType roomType)
    {
        return new IsBookingAllowedQueryHandler(
                _employeeRepository,
                _companyPolicyRepository,
                _employeePolicyRepository)
            .Handle(new IsBookingAllowedQuery(employeeId, roomType));
    }
}