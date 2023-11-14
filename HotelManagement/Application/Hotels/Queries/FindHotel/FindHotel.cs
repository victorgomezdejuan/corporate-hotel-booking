
using HotelManagement.Repositories.Hotels;

namespace HotelManagement.Application.Hotels.Queries.FindHotel;

public record FindHotelQuery
{
    public int Id { get; init; }
}

public class FindHotelQueryHandler
{
    private IHotelRepository _hotelRepository;

    public FindHotelQueryHandler(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public HotelDto Handle(FindHotelQuery query)
    {
        var hotel = _hotelRepository.GetHotelWithRooms(query.Id);
        return new HotelDto(hotel.Id, hotel.Rooms.Select(x => new RoomDto(x.Number, x.Type)).ToList());
    }
}