using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Employees;

namespace CorporateHotelBooking.Application.Employees.Commands.AddEmployee;

public record AddEmployeeCommand
{
    public AddEmployeeCommand(int employeeID, int companyId)
    {
        EmployeeId = employeeID;
        CompanyId = companyId;
    }

    public int EmployeeId { get; }
    public int CompanyId { get; }
}

public class AddEmployeeCommandHandler
{
    private IEmployeeRepository _employeeRepository;

    public AddEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public void Handle(AddEmployeeCommand command)
    {
        if (_employeeRepository.Exists(command.EmployeeId))
        {
            throw new EmployeeAlreadyExistsException();
        }
        _employeeRepository.Add(new Employee(command.EmployeeId, command.CompanyId));
    }
}