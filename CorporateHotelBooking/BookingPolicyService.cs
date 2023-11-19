using CorporateHotelBooking.Application.CompanyPolicies.Commands.SetCompanyPolicy;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;

namespace CorporateHotelBooking;

public class BookingPolicyService
{
    private InMemoryCompanyPolicyRepository _companyPolicyRepository;

    public BookingPolicyService(InMemoryCompanyPolicyRepository companyPolicyRepository)
    {
        _companyPolicyRepository = companyPolicyRepository;
    }

    public void SetCompanyPolicy(int companyId, ICollection<RoomType> roomTypes)
    {
        new SetCompanyPolicyCommandHandler(_companyPolicyRepository).Handle(new SetCompanyPolicyCommand(companyId, roomTypes));
    }

    public void SetEmployeePolicy(int employeeId, ICollection<RoomType> roomTypes)
    {
        throw new NotImplementedException();
    }

    public bool IsBookingAllowed(int employeeId, RoomType roomType)
    {
        throw new NotImplementedException();
    }
}