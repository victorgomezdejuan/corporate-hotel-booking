using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Repositories.CompanyPolicies;

public class InMemoryCompanyPolicyRepository : ICompanyPolicyRepository
{
    private readonly Dictionary<int, CompanyPolicy> _companyPolicies;

    public InMemoryCompanyPolicyRepository()
    {
        _companyPolicies = new();
    }

    public void AddCompanyPolicy(CompanyPolicy companyPolicy)
    {
        _companyPolicies.Add(companyPolicy.CompanyId, companyPolicy);
    }

    public CompanyPolicy GetCompanyPolicy(int companyId)
    {
        return _companyPolicies[companyId];
    }
}