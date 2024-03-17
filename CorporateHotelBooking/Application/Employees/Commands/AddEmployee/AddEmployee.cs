using CorporateHotelBooking.Application.Common;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Employees;

namespace CorporateHotelBooking.Application.Employees.Commands.AddEmployee;

public record AddEmployeeCommand(int EmployeeId, int CompanyId);

public class AddEmployeeCommandHandler
{
    private IEmployeeRepository _employeeRepository;

    public AddEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public Result Handle(AddEmployeeCommand command)
    {
        if (_employeeRepository.Exists(command.EmployeeId))
        {
            return Result.Failure("Employee already exists in the company");
        }

        _employeeRepository.Add(new Employee(command.EmployeeId, command.CompanyId));
        
        return Result.Success();
    }
}