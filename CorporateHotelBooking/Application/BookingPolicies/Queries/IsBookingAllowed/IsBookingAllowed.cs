using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;
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

    public IsBookingAllowedQueryHandler(IEmployeeRepository employeeRepository, ICompanyPolicyRepository companyPolicyRepository)
    {
        _employeeRepository = employeeRepository;
        _companyPolicyRepository = companyPolicyRepository;
    }

    public bool Handle(IsBookingAllowedQuery query)
    {
        var employee = _employeeRepository.GetEmployee(query.EmployeeId);
        var companyPolicy = _companyPolicyRepository.GetCompanyPolicy(employee.CompanyId);
        return companyPolicy.AllowedRoomTypes.Contains(query.RoomType);
    }
}