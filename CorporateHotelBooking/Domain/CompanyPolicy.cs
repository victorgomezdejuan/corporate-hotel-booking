namespace CorporateHotelBooking.Domain;

public class CompanyPolicy
{
    public CompanyPolicy(int companyId, ICollection<RoomType> roomTypes)
    {
        CompanyId = companyId;
        RoomTypes = roomTypes.ToList().AsReadOnly();
    }

    public int CompanyId { get; }
    public IReadOnlyCollection<RoomType> RoomTypes { get; }

    public override bool Equals(object? obj)
    {
        return obj is CompanyPolicy policy &&
               CompanyId == policy.CompanyId &&
               RoomTypes.SequenceEqual(policy.RoomTypes);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(CompanyId, RoomTypes);
    }
}