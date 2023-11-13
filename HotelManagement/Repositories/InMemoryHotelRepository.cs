
using HotelManagement.Application;
using HotelManagement.Domain;

namespace HotelManagement.Repositories;

public class InMemoryHotelRepository : IHotelRepository
{
    private readonly Dictionary<int, Hotel> _hotels;

    public InMemoryHotelRepository()
    {
        _hotels = new Dictionary<int, Hotel>();
    }

    public void AddHotel(Hotel hotel)
    {
        _hotels.Add(hotel.Id, hotel);
    }

    public Hotel GetHotel(int hotelId)
    {
        if (!_hotels.ContainsKey(hotelId))
        {
            throw new HotelNotFoundException(hotelId);
        }
        
        return new Hotel(_hotels[hotelId].Id, _hotels[hotelId].Name);
    }

    public bool Exists(int hotelId)
    {
        return _hotels.ContainsKey(hotelId);
    }
}