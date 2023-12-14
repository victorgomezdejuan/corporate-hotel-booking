using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.Employees;

namespace CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;

public record DeleteEmployeeCommand
{
    public int EmployeeId { get; }

    public DeleteEmployeeCommand(int employeeId)
    {
        EmployeeId = employeeId;
    }
}

public class DeleteEmployeeCommandHandler
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IBookingRepository _bookingRepository;

    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IBookingRepository bookingRepository)
    {
        _employeeRepository = employeeRepository;
        _bookingRepository = bookingRepository;
    }

    public void Handle(DeleteEmployeeCommand command)
    {
        if (!_employeeRepository.Exists(command.EmployeeId))
        {
            throw new EmployeeNotFoundException(command.EmployeeId);
        }
        _bookingRepository.DeleteByEmployeeId(command.EmployeeId);
        _employeeRepository.Delete(command.EmployeeId);
    }
}