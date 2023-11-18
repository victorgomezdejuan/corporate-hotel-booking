namespace CorporateHotelBooking.Application.Rooms.Commands.SetRoom;

public class HotelNotFoundException : Exception
{
    public HotelNotFoundException(int hotelId) : base($"Hotel with id {hotelId} not found")
    {
        HotelId = hotelId;
    }

    public int HotelId { get; }
}