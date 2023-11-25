namespace CorporateHotelBooking.Domain;

public class EmployeeBookingPolicy
{
    public EmployeeBookingPolicy(int employeeId, ICollection<RoomType> allowedRoomTypes)
    {
        EmployeeId = employeeId;
        AllowedRoomTypes = allowedRoomTypes.ToList().AsReadOnly();
    }

    public int EmployeeId { get; }
    public IReadOnlyCollection<RoomType> AllowedRoomTypes { get; }

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