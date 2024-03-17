using CorporateHotelBooking.Application.Common;
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

    public Result AddEmployee(int companyId, int employeeId)
    {
        return new AddEmployeeCommandHandler(_employeeRepository).Handle(new AddEmployeeCommand(employeeId, companyId));
    }

    public void DeleteEmployee(int employeeId)
    {
        var handler = new DeleteEmployeeCommandHandler(_employeeRepository);
        handler.Subscribe(new EmployeeBookingDeleter(_bookingRepository));
        handler.Subscribe(new EmployeeBookingPoliciesDeleter(_employeeBookingPolicyRepository));
        handler.Handle(new DeleteEmployeeCommand(employeeId));
    }
}