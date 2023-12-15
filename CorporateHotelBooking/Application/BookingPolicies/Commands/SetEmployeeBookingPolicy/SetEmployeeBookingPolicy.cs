using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;

namespace CorporateHotelBooking.Application.BookingPolicies.Commands.SetEmployeeBookingPolicy
{
    public record SetEmployeeBookingPolicyCommand
    {
        public SetEmployeeBookingPolicyCommand(int employeeId, IEnumerable<RoomType> roomTypes)
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

        public void Handle(SetEmployeeBookingPolicyCommand command)
        {
            var employeePolicy = new EmployeeBookingPolicy(command.EmployeeId, command.RoomTypes);

            if (_employeePolicyRepository.Exists(command.EmployeeId))
            {
                _employeePolicyRepository.Update(employeePolicy);
            }
            else
            {
                _employeePolicyRepository.Add(employeePolicy);
            }
        }
    }
}