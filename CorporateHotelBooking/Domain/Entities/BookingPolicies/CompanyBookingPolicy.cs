namespace CorporateHotelBooking.Domain.Entities.BookingPolicies;

public class CompanyBookingPolicy : BookingPolicy
{
    public CompanyBookingPolicy(int companyId, IEnumerable<RoomType> allowedRoomTypes) : base(true)
    {
        CompanyId = companyId;
        AllowedRoomTypes = allowedRoomTypes.ToList().AsReadOnly();
    }

    public int CompanyId { get; }
    public IReadOnlyCollection<RoomType> AllowedRoomTypes { get; }

    protected override bool BookingAllowedForRoomType(RoomType roomType)
    {
        return AllowedRoomTypes.Contains(roomType);
    }

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