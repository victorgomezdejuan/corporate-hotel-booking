using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking;

public class BookingPolicyService
{
    public void SetCompanyPolicy(int companyId, ICollection<RoomType> roomTypes)
    {
        throw new NotImplementedException();
    }

    public void SetEmployeePolicy(int employeeId, ICollection<RoomType> roomTypes)
    {
        throw new NotImplementedException();
    }

    public bool IsBookingAllowed(int employeeId, RoomType roomType)
    {
        throw new NotImplementedException();
    }
}