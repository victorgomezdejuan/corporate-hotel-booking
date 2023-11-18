using System.Collections.ObjectModel;
using CorporateHotelBooking.Application.Hotels.Queries.FindHotel;

namespace CorporateHotelBooking.Application;

public class HotelDto
{
    public int Id { get; }

    public string Name { get; }

    public ReadOnlyCollection<RoomDto> Rooms { get; }

    public HotelDto(int id, string name, ICollection<RoomDto> rooms)
    {
        Id = id;
        Name = name;
        Rooms = rooms.ToList().AsReadOnly();
    }
}