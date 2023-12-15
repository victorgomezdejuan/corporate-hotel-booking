using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Repositories.Hotels;

public class InMemoryHotelRepository : IHotelRepository
{
    private readonly Dictionary<int, Hotel> _hotels;

    public InMemoryHotelRepository()
    {
        _hotels = new Dictionary<int, Hotel>();
    }

    public void Add(Hotel hotel)
    {
        _hotels.Add(hotel.Id, hotel);
    }

    public bool Exists(int hotelId)
    {
        return _hotels.ContainsKey(hotelId);
    }

    public Hotel Get(int hotelId)
    {      
        return new Hotel(_hotels[hotelId].Id, _hotels[hotelId].Name);
    }
}