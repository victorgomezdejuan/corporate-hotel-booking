using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;

namespace CorporateHotelBooking.Application.BookingPolicies.Commands.SetEmployeeBookingPolicy
{
    public record SetEmployeeBookingPolicyCommand
    {
        public SetEmployeeBookingPolicyCommand(int employeeId, ICollection<RoomType> roomTypes)
        {
            EmployeeId = employeeId;
            RoomTypes = roomTypes.ToList().AsReadOnly();
        }

        public int EmployeeId { get; }
        public IReadOnlyCollection<RoomType> RoomTypes { get; }
    }

    public class SetEmployeeBookingPolicyCommandHandler
    {
        private IEmployeeBookingPolicyRepository _employeePolicyRepository;

        public SetEmployeeBookingPolicyCommandHandler(IEmployeeBookingPolicyRepository employeePolicyRepository)
        {
            _employeePolicyRepository = employeePolicyRepository;
        }

        public void Handle(SetEmployeeBookingPolicyCommand setEmployeePolicyCommand)
        {
            var employeePolicy = new EmployeeBookingPolicy(setEmployeePolicyCommand.EmployeeId, setEmployeePolicyCommand.RoomTypes.ToList());

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