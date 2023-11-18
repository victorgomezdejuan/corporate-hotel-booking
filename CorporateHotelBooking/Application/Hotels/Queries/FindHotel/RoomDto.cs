using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Application.Hotels.Queries.FindHotel;

public record RoomDto
{
    public int Number { get; }

    public RoomType Type { get; }

    public RoomDto(int number, RoomType type)
    {
        Number = number;
        Type = type;
    }
}