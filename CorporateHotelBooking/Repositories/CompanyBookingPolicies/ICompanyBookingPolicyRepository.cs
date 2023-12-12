using CorporateHotelBooking.Domain.Entities.BookingPolicies;

namespace CorporateHotelBooking.Repositories.CompanyBookingPolicies;

public interface ICompanyBookingPolicyRepository
{
    void Add(CompanyBookingPolicy companyPolicy);
    void UpdateCompanyPolicy(CompanyBookingPolicy companyPolicy);
    bool Exists(int companyId);
    CompanyBookingPolicy GetCompanyPolicy(int companyId);
}