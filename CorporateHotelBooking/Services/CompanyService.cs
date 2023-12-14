using CorporateHotelBooking.Application.Employees.Commands.AddEmployee;
using CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;

namespace CorporateHotelBooking.Services;

public class CompanyService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IEmployeeBookingPolicyRepository _employeeBookingPolicyRepository;

    public CompanyService(
        IEmployeeRepository employeeRepository,
        IBookingRepository bookingRepository,
        IEmployeeBookingPolicyRepository employeeBookingPolicyRepository)
    {
        _employeeRepository = employeeRepository;
        _bookingRepository = bookingRepository;
        _employeeBookingPolicyRepository = employeeBookingPolicyRepository;
    }

    public void AddEmployee(int companyId, int employeeId)
    {
        new AddEmployeeCommandHandler(_employeeRepository).Handle(new AddEmployeeCommand(employeeId, companyId));
    }

    public void DeleteEmployee(int employeeId)
    {
        new DeleteEmployeeCommandHandler(_employeeRepository, _bookingRepository, _employeeBookingPolicyRepository)
            .Handle(new DeleteEmployeeCommand(employeeId));
    }
}