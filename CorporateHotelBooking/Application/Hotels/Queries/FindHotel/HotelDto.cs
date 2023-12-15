using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Application;

public record HotelDto
{
    public HotelDto(int id, string name, IEnumerable<RoomDto> rooms)
    {
        Id = id;
        Name = name;
        Rooms = rooms.ToList().AsReadOnly();
    }

    public int Id { get; }
    public string Name { get; }
    public IReadOnlyCollection<RoomDto> Rooms { get; }
}

public record RoomDto(int Number, RoomType Type);