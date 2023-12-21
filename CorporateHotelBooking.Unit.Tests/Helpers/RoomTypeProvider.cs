using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Unit.Tests.Helpers;

public class RoomTypeProvider
{
    public static RoomType NotContainedIn(List<RoomType> roomTypes)
    {
        var differentRoomType = RoomType.Standard;
        while (roomTypes.Contains(differentRoomType))
        {
            differentRoomType++;
        }

        return differentRoomType;
    }

    public static RoomType DifferentFrom(RoomType roomType)
    {
        var differentRoomType = RoomType.Standard;
        while (differentRoomType == roomType)
        {
            differentRoomType++;
        }

        return differentRoomType;
    }
}