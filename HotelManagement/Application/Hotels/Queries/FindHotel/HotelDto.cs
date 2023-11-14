using System.Collections.ObjectModel;
using HotelManagement.Application.Hotels.Queries.FindHotel;

namespace HotelManagement.Application;

public class HotelDto
{
    public int Id { get; }

    public ReadOnlyCollection<RoomDto> Rooms { get; }

    public HotelDto(int id, ICollection<RoomDto> rooms)
    {
        Id = id;
        Rooms = rooms.ToList().AsReadOnly();
    }
}