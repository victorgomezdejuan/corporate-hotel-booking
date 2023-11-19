namespace CorporateHotelBooking.Domain;

public class CompanyPolicy
{
    public CompanyPolicy(int companyId, ICollection<RoomType> allowedRoomTypes)
    {
        CompanyId = companyId;
        AllowedRoomTypes = allowedRoomTypes.ToList().AsReadOnly();
    }

    public int CompanyId { get; }
    public IReadOnlyCollection<RoomType> AllowedRoomTypes { get; }

    public override bool Equals(object? obj)
    {
        return obj is CompanyPolicy policy &&
               CompanyId == policy.CompanyId &&
               AllowedRoomTypes.SequenceEqual(policy.AllowedRoomTypes);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(CompanyId, AllowedRoomTypes);
    }
}