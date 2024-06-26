using CorporateHotelBooking.Application.Common;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;

namespace CorporateHotelBooking.Application.BookingPolicies.Queries.IsBookingAllowed;

public record IsBookingAllowedQuery(int EmployeeId, RoomType RoomType);

public class IsBookingAllowedQueryHandler
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICompanyBookingPolicyRepository _companyPolicyRepository;
    private readonly IEmployeeBookingPolicyRepository _employeePolicyRepository;

    public IsBookingAllowedQueryHandler(
        IEmployeeRepository employeeRepository,
        ICompanyBookingPolicyRepository companyPolicyRepository,
        IEmployeeBookingPolicyRepository employeePolicyRepository)
    {
        _employeeRepository = employeeRepository;
        _companyPolicyRepository = companyPolicyRepository;
        _employeePolicyRepository = employeePolicyRepository;
    }

    public Result<bool> Handle(IsBookingAllowedQuery query)
    {
        if (!_employeeRepository.Exists(query.EmployeeId))
        {
            return Result<bool>.Failure("Employee not found.");
        }

        BookingPolicy employeeBookingPolicy= _employeePolicyRepository.Exists(query.EmployeeId)
            ? _employeePolicyRepository.Get(query.EmployeeId)!
            : new NonApplicableBookingPolicy();
        BookingPolicy companyBookingPolicy = _companyPolicyRepository.Exists(query.EmployeeId)
            ? _companyPolicyRepository.Get(_employeeRepository.Get(query.EmployeeId)!.CompanyId)
            : new NonApplicableBookingPolicy();

        var aggregatedBookingPolicy = new AggregatedBookingPolicy(employeeBookingPolicy, companyBookingPolicy);

        return Result<bool>.Success(aggregatedBookingPolicy.BookingAllowed(query.RoomType));
    }
}