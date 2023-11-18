using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Employees;

namespace CorporateHotelBooking.Application.Employees;

public class AddEmployeeCommand
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
        _employeeRepository.AddEmployee(new Employee(command.EmployeeId, command.CompanyId));
    }
}