using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Unit.Tests.Helpers;

public class RoomTypeProvider
{
    public static RoomType NotContainedIn(List<RoomType> roomTypes)
    {
        var roomType = RoomType.Standard;
        while (roomTypes.Contains(roomType))
        {
            roomType++;
        }

        return roomType;
    }
}