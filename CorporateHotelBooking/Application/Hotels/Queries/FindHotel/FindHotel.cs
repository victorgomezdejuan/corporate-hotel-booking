
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Application.Hotels.Queries.FindHotel;

public record FindHotelQuery(int Id);

public class FindHotelQueryHandler
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomRepository _roomRepository;

    public FindHotelQueryHandler(IHotelRepository hotelRepository, IRoomRepository rommRepository)
    {
        _hotelRepository = hotelRepository;
        _roomRepository = rommRepository;
    }

    public HotelDto? Handle(FindHotelQuery query)
    {
        var hotel = _hotelRepository.Get(query.Id);

        if (hotel is null)
        {
            return null;
        }

        var rooms = _roomRepository.GetMany(query.Id).Select(r => new RoomDto(r.Number, r.Type));

        return new HotelDto(hotel.Id, hotel.Name, rooms);
    }
}