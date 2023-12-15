using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Application.Bookings.Commands;

public class RoomTypeNotProvidedByTheHotelException : Exception
{
    public RoomTypeNotProvidedByTheHotelException(int hotelId, RoomType roomType)
    {
        HotelId = hotelId;
        RoomType = roomType;
    }

    public int HotelId { get; }
    public RoomType RoomType { get; }
}