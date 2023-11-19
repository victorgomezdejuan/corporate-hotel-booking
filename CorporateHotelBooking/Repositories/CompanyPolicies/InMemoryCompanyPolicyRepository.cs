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

    public void UpdateCompanyPolicy(CompanyPolicy companyPolicy)
    {
        _companyPolicies[companyPolicy.CompanyId] = companyPolicy;
    }

    public bool Exists(int companyId)
    {
        return _companyPolicies.ContainsKey(companyId);
    }

    public CompanyPolicy GetCompanyPolicy(int companyId)
    {
        return _companyPolicies[companyId];
    }
}