using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Repositories.Employees;

namespace CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;

public record DeleteEmployeeCommand(int EmployeeId);

public class DeleteEmployeeCommandHandler
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly List<IEmployeeDeletedObserver> _observers = new();

    public DeleteEmployeeCommandHandler(
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public void Handle(DeleteEmployeeCommand command)
    {
        if (!_employeeRepository.Exists(command.EmployeeId))
        {
            throw new EmployeeNotFoundException(command.EmployeeId);
        }
        _employeeRepository.Delete(command.EmployeeId);
        Notify(command.EmployeeId);
        DeleteTheirAssociatedItems(command);
    }

    public void Subscribe(IEmployeeDeletedObserver observer)
    {
        _observers.Add(observer);
    }

    private void Notify(int employeeId)
    {
        _observers.ForEach(o => o.Notify(employeeId));
    }

    private void DeleteTheirAssociatedItems(DeleteEmployeeCommand command)
    {
        //_bookingRepository.DeleteByEmployee(command.EmployeeId);
        //_employeeBookingPolicyRepository.Delete(command.EmployeeId);
    }
}