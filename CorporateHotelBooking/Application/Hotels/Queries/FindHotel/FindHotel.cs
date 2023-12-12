
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Application.Hotels.Queries.FindHotel;

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
        var hotel = _hotelRepository.Get(query.Id);
        var rooms = _roomRepository.GetMany(query.Id).Select(x => new RoomDto(x.Number, x.Type)).ToList();
        return new HotelDto(hotel.Id, hotel.Name, rooms);
    }
}