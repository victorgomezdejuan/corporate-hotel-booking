using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking;

public class BookingPolicyService
{
    public void SetCompanyPolicy(int companyId, IReadOnlyCollection<RoomType> roomTypes)
    {
        throw new NotImplementedException();
    }

    public void SetEmployeePolicy(int employeeId, IReadOnlyCollection<RoomType> roomTypes)
    {
        throw new NotImplementedException();
    }

    public bool IsBookingAllowed(int employeeId, RoomType roomType)
    {
        throw new NotImplementedException();
    }
}