using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;
using CorporateHotelBooking.Repositories.EmployeePolicies;
using CorporateHotelBooking.Repositories.Employees;

namespace CorporateHotelBooking.Application.BookingPolicies.Queries.IsBookingAllowed;

public record IsBookingAllowedQuery
{
    public IsBookingAllowedQuery(int employeeId, RoomType roomType)
    {
        EmployeeId = employeeId;
        RoomType = roomType;
    }

    public int EmployeeId { get; }
    public RoomType RoomType { get; }
}

public class IsBookingAllowedQueryHandler
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICompanyPolicyRepository _companyPolicyRepository;
    private readonly IEmployeePolicyRepository _employeePolicyRepository;

    public IsBookingAllowedQueryHandler(
        IEmployeeRepository employeeRepository,
        ICompanyPolicyRepository companyPolicyRepository,
        IEmployeePolicyRepository employeePolicyRepository)
    {
        _employeeRepository = employeeRepository;
        _companyPolicyRepository = companyPolicyRepository;
        _employeePolicyRepository = employeePolicyRepository;
    }

    public bool Handle(IsBookingAllowedQuery query)
    {
        if (!_employeeRepository.Exists(query.EmployeeId))
        {
            throw new EmployeeNotFoundException(query.EmployeeId);
        }

        if (_employeePolicyRepository.Exists(query.EmployeeId))
        {
            if (_employeePolicyRepository.GetEmployeePolicy(query.EmployeeId).AllowedRoomTypes.Contains(query.RoomType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        var employee = _employeeRepository.GetEmployee(query.EmployeeId);

        if (_companyPolicyRepository.Exists(employee.CompanyId))
        {
            if (_companyPolicyRepository.GetCompanyPolicy(employee.CompanyId).AllowedRoomTypes.Contains(query.RoomType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}