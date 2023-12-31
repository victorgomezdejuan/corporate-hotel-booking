using CorporateHotelBooking.Domain.Entities.BookingPolicies;

namespace CorporateHotelBooking.Repositories.CompanyBookingPolicies;

public class InMemoryCompanyBookingPolicyRepository : ICompanyBookingPolicyRepository
{
    private readonly Dictionary<int, CompanyBookingPolicy> _companyPolicies;

    public InMemoryCompanyBookingPolicyRepository()
    {
        _companyPolicies = new();
    }

    public void Add(CompanyBookingPolicy companyPolicy)
    {
        _companyPolicies.Add(companyPolicy.CompanyId, companyPolicy);
    }

    public void Update(CompanyBookingPolicy companyPolicy)
    {
        _companyPolicies[companyPolicy.CompanyId] = companyPolicy;
    }

    public bool Exists(int companyId)
    {
        return _companyPolicies.ContainsKey(companyId);
    }

    public CompanyBookingPolicy Get(int companyId)
    {
        return _companyPolicies[companyId];
    }
}