namespace CorporateHotelBooking.Domain.Entities.BookingPolicies;

public class NonApplicableBookingPolicy : BookingPolicy
{
    public NonApplicableBookingPolicy() : base(false) { }

    protected override bool BookingAllowedForRoomType(RoomType roomType)
    {
        return false;
    }
}