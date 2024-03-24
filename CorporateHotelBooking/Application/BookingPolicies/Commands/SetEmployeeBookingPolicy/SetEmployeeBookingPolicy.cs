using CorporateHotelBooking.Application.Common;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;

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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeBookingPolicyRepository _employeePolicyRepository;

        public SetEmployeeBookingPolicyCommandHandler(
            IEmployeeRepository employeeRepository,
            IEmployeeBookingPolicyRepository employeePolicyRepository)
        {
            _employeeRepository = employeeRepository;
            _employeePolicyRepository = employeePolicyRepository;
        }

        public Result Handle(SetEmployeeBookingPolicyCommand command)
        {
            if (!_employeeRepository.Exists(command.EmployeeId))
            {
                return Result.Failure("Employee not found");
            }

            var employeePolicy = new EmployeeBookingPolicy(command.EmployeeId, command.RoomTypes);

            if (_employeePolicyRepository.Exists(command.EmployeeId))
            {
                _employeePolicyRepository.Update(employeePolicy);
            }
            else
            {
                _employeePolicyRepository.Add(employeePolicy);
            }

            return Result.Success();
        }
    }
}