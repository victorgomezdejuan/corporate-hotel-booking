namespace CorporateHotelBooking.Domain.Entities;

public class EmployeeBookingPolicy : BookingPolicy
{
    public EmployeeBookingPolicy(int employeeId, ICollection<RoomType> allowedRoomTypes) : base(true)
    {
        EmployeeId = employeeId;
        AllowedRoomTypes = allowedRoomTypes.ToList().AsReadOnly();
    }

    public int EmployeeId { get; }
    public IReadOnlyCollection<RoomType> AllowedRoomTypes { get; }

    protected override bool BookingAllowedForRoomType(RoomType roomType)
    {
        return AllowedRoomTypes.Contains(roomType);
    }

    public override bool Equals(object? obj)
    {
        return obj is EmployeeBookingPolicy policy &&
               EmployeeId == policy.EmployeeId &&
               AllowedRoomTypes.SequenceEqual(policy.AllowedRoomTypes);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(EmployeeId, AllowedRoomTypes);
    }
}