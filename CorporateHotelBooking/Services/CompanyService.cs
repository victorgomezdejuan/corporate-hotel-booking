using CorporateHotelBooking.Application.Employees.Commands.AddEmployee;
using CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.Employees;

namespace CorporateHotelBooking.Services;

public class CompanyService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IBookingRepository _bookingRepository;

    public CompanyService(IEmployeeRepository employeeRepository, IBookingRepository bookingRepository)
    {
        _employeeRepository = employeeRepository;
        _bookingRepository = bookingRepository;
    }

    public void AddEmployee(int companyId, int employeeId)
    {
        new AddEmployeeCommandHandler(_employeeRepository).Handle(new AddEmployeeCommand(employeeId, companyId));
    }

    public void DeleteEmployee(int employeeId)
    {
        new DeleteEmployeeCommandHandler(_employeeRepository, _bookingRepository).Handle(new DeleteEmployeeCommand(employeeId));
    }
}