using CorporateHotelBooking.Application.Employees;
using CorporateHotelBooking.Repositories.Employees;

namespace CorporateHotelBooking;

public class CompanyService
{
    private IEmployeeRepository _employeeRepository;

    public CompanyService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public void AddEmployee(int companyId, int employeeId)
    {
        new AddEmployeeCommandHandler(_employeeRepository).Handle(new AddEmployeeCommand(employeeId, companyId));
    }

    public void DeleteEmployee(int employeeId)
    {
        throw new NotImplementedException();
    }
}