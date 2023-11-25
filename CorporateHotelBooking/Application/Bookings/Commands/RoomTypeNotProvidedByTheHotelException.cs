using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Application.Bookings.Commands;

public class RoomTypeNotProvidedByTheHotelException : Exception
{
    private int hotelId;
    private RoomType roomType;

    public RoomTypeNotProvidedByTheHotelException(int hotelId, RoomType roomType)
    {
        this.hotelId = hotelId;
        this.roomType = roomType;
    }
}