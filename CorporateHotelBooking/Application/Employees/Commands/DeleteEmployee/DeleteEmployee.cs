using CorporateHotelBooking.Application.Common.Exceptions;
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
    private IEmployeeRepository _employeeRepository;

    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public void Handle(DeleteEmployeeCommand command)
    {
        if (!_employeeRepository.Exists(command.EmployeeId))
        {
            throw new EmployeeNotFoundException(command.EmployeeId);
        }
        _employeeRepository.DeleteEmployee(command.EmployeeId);
    }
}