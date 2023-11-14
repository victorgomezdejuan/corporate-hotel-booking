
using HotelManagement.Repositories.Hotels;
using HotelManagement.Repositories.Rooms;

namespace HotelManagement.Application.Hotels.Queries.FindHotel;

public record FindHotelQuery
{
    public int Id { get; }

    public FindHotelQuery(int id)
    {
        Id = id;
    }
}

public class FindHotelQueryHandler
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomRepository _roomRepository;

    public FindHotelQueryHandler(IHotelRepository hotelRepository, IRoomRepository rommRepository)
    {
        _hotelRepository = hotelRepository;
        _roomRepository = rommRepository;

    }

    public HotelDto Handle(FindHotelQuery query)
    {
        var hotel = _hotelRepository.GetHotel(query.Id);
        var rooms = _roomRepository.GetRooms(query.Id).Select(x => new RoomDto(x.Number, x.Type)).ToList();
        return new HotelDto(hotel.Id, hotel.Name, rooms);
    }
}