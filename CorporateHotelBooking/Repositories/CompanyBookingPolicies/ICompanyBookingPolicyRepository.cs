using CorporateHotelBooking.Domain.Entities.BookingPolicies;

namespace CorporateHotelBooking.Repositories.CompanyBookingPolicies;

public interface ICompanyBookingPolicyRepository
{
    void Add(CompanyBookingPolicy companyPolicy);
    void Update(CompanyBookingPolicy companyPolicy);
    bool Exists(int companyId);
    CompanyBookingPolicy Get(int companyId);
}