using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
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

    public bool Handle(IsBookingAllowedQuery query)
    {
        if (!_employeeRepository.Exists(query.EmployeeId))
        {
            throw new EmployeeNotFoundException(query.EmployeeId);
        }

        if (_employeePolicyRepository.Exists(query.EmployeeId))
        {
            return IsBookingAllowedByEmployeeBookingPolicy(query);
        }

        var employee = _employeeRepository.GetEmployee(query.EmployeeId);

        if (_companyPolicyRepository.Exists(employee.CompanyId))
        {
            return IsBookingAllowedByCompanyBookingPolicy(query, employee);
        }

        return true;
    }

    private bool IsBookingAllowedByCompanyBookingPolicy(IsBookingAllowedQuery query, Employee employee)
    {
        return _companyPolicyRepository.GetCompanyPolicy(employee.CompanyId).AllowedRoomTypes.Contains(query.RoomType);
    }

    private bool IsBookingAllowedByEmployeeBookingPolicy(IsBookingAllowedQuery query)
    {
        return _employeePolicyRepository.GetEmployeePolicy(query.EmployeeId).AllowedRoomTypes.Contains(query.RoomType);
    }
}