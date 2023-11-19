using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Repositories.CompanyPolicies;

public interface ICompanyPolicyRepository
{
    void AddCompanyPolicy(CompanyPolicy companyPolicy);
    CompanyPolicy GetCompanyPolicy(int companyId);
}