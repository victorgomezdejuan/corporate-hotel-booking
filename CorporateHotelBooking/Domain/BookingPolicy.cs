namespace CorporateHotelBooking.Domain;

public abstract class BookingPolicy
{
    public BookingPolicy(bool isApplicable)
    {
        IsApplicable = isApplicable;
    }

    public bool IsApplicable { get; }

    public bool BookingAllowed(RoomType roomType)
    {
        if (!IsApplicable)
        {
            throw new InvalidOperationException("Booking policy is not applicable");
        }
        return BookingAllowedForRoomType(roomType);
    }

    protected abstract bool BookingAllowedForRoomType(RoomType roomType);
}