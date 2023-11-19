using System.Collections.ObjectModel;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;

namespace CorporateHotelBooking.Application.CompanyPolicies.Commands.SetCompanyPolicy;

public class SetCompanyPolicyCommand
{
    public SetCompanyPolicyCommand(int companyId, ICollection<RoomType> roomTypes)
    {
        CompanyId = companyId;
        RoomTypes = roomTypes.ToList().AsReadOnly();
    }

    public int CompanyId { get; }
    public IReadOnlyCollection<RoomType> RoomTypes { get; }
}

public class SetCompanyPolicyCommandHandler
{
    private ICompanyPolicyRepository _companyPolicyRepository;

    public SetCompanyPolicyCommandHandler(ICompanyPolicyRepository companyPolicyRepository)
    {
        _companyPolicyRepository = companyPolicyRepository;
    }

    public void Handle(SetCompanyPolicyCommand command)
    {
        _companyPolicyRepository.AddCompanyPolicy(new CompanyPolicy(command.CompanyId, command.RoomTypes.ToList()));
    }
}