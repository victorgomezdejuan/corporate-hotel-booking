namespace CorporateHotelBooking.Domain;

public class CompanyBookingPolicy
{
    public CompanyBookingPolicy(int companyId, ICollection<RoomType> allowedRoomTypes)
    {
        CompanyId = companyId;
        AllowedRoomTypes = allowedRoomTypes.ToList().AsReadOnly();
    }

    public int CompanyId { get; }
    public IReadOnlyCollection<RoomType> AllowedRoomTypes { get; }

    public override bool Equals(object? obj)
    {
        return obj is CompanyBookingPolicy policy &&
               CompanyId == policy.CompanyId &&
               AllowedRoomTypes.SequenceEqual(policy.AllowedRoomTypes);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(CompanyId, AllowedRoomTypes);
    }
}