using CorporateHotelBooking.Application.Common;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;

namespace CorporateHotelBooking.Application.BookingPolicies.Commands.SetCompanyBookingPolicy;

public record SetCompanyBookingPolicyCommand
{
    public SetCompanyBookingPolicyCommand(int companyId, IEnumerable<RoomType> roomTypes)
    {
        CompanyId = companyId;
        RoomTypes = roomTypes.ToList().AsReadOnly();
    }

    public int CompanyId { get; }
    public IReadOnlyCollection<RoomType> RoomTypes { get; }
}

public class SetCompanyBookingPolicyCommandHandler
{
    private ICompanyBookingPolicyRepository _companyPolicyRepository;

    public SetCompanyBookingPolicyCommandHandler(ICompanyBookingPolicyRepository companyPolicyRepository)
    {
        _companyPolicyRepository = companyPolicyRepository;
    }

    public Result Handle(SetCompanyBookingPolicyCommand command)
    {
        var companyPoliciy = new CompanyBookingPolicy(command.CompanyId, command.RoomTypes);

        if (_companyPolicyRepository.Exists(command.CompanyId))
        {
            _companyPolicyRepository.Update(companyPoliciy);
        }
        else
        {
            _companyPolicyRepository.Add(companyPoliciy);
        }

        return Result.Success();
    }
}