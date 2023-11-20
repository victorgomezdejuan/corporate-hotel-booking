using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.EmployeePolicies;

namespace CorporateHotelBooking.Application.EmployeePolicies.Commands.SetEmployeePolicy
{
    public record SetEmployeePolicyCommand
    {
        public SetEmployeePolicyCommand(int employeeId, List<RoomType> roomTypes)
        {
            EmployeeId = employeeId;
            RoomTypes = roomTypes;
        }

        public int EmployeeId { get; }
        public List<RoomType> RoomTypes { get; }
    }

    public class SetEmployeePolicyCommandHandler
    {
        private IEmployeePolicyRepository _employeePolicyRepository;

        public SetEmployeePolicyCommandHandler(IEmployeePolicyRepository employeePolicyRepository)
        {
            _employeePolicyRepository = employeePolicyRepository;
        }

        public void Handle(SetEmployeePolicyCommand setEmployeePolicyCommand)
        {
            var employeePolicy = new EmployeePolicy(setEmployeePolicyCommand.EmployeeId, setEmployeePolicyCommand.RoomTypes);

            if (_employeePolicyRepository.Exists(setEmployeePolicyCommand.EmployeeId))
            {
                _employeePolicyRepository.UpdateEmployeePolicy(employeePolicy);
            }
            else
            {
                _employeePolicyRepository.AddEmployeePolicy(employeePolicy);
            }
        }
    }
}