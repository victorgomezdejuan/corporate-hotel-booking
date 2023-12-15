using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;

namespace CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;

public record DeleteEmployeeCommand(int EmployeeId);

public class DeleteEmployeeCommandHandler
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IEmployeeBookingPolicyRepository _employeeBookingPolicyRepository;

    public DeleteEmployeeCommandHandler(
        IEmployeeRepository employeeRepository,
        IBookingRepository bookingRepository,
        IEmployeeBookingPolicyRepository employeeBookingPolicyRepository)
    {
        _employeeRepository = employeeRepository;
        _bookingRepository = bookingRepository;
        _employeeBookingPolicyRepository = employeeBookingPolicyRepository;
    }

    public void Handle(DeleteEmployeeCommand command)
    {
        if (!_employeeRepository.Exists(command.EmployeeId))
        {
            throw new EmployeeNotFoundException(command.EmployeeId);
        }
        DeleteEmployeeAndTheirAssociatedItems(command);
    }

    private void DeleteEmployeeAndTheirAssociatedItems(DeleteEmployeeCommand command)
    {
        _bookingRepository.DeleteByEmployee(command.EmployeeId);
        _employeeBookingPolicyRepository.Delete(command.EmployeeId);
        _employeeRepository.Delete(command.EmployeeId);
    }
}