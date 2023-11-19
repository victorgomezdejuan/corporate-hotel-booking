using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Repositories.CompanyPolicies;

public interface ICompanyPolicyRepository
{
    void AddCompanyPolicy(CompanyPolicy companyPolicy);
    void UpdateCompanyPolicy(CompanyPolicy companyPolicy);
    bool Exists(int companyId);
    CompanyPolicy GetCompanyPolicy(int companyId);
}