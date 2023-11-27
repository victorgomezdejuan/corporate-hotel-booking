using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;

namespace CorporateHotelBooking.Application.BookingPolicies.Commands.SetCompanyBookingPolicy;

public class SetCompanyBookingPolicyCommand
{
    public SetCompanyBookingPolicyCommand(int companyId, ICollection<RoomType> roomTypes)
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

    public void Handle(SetCompanyBookingPolicyCommand command)
    {
        if (_companyPolicyRepository.Exists(command.CompanyId))
        {
            _companyPolicyRepository.UpdateCompanyPolicy(new CompanyBookingPolicy(command.CompanyId, command.RoomTypes.ToList()));
        }
        else
        {
            _companyPolicyRepository.AddCompanyPolicy(new CompanyBookingPolicy(command.CompanyId, command.RoomTypes.ToList()));
        }
    }
}