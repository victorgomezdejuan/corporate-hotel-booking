namespace HotelService;

public class InMemoryHotelRepository : IHotelRepository
{
    private readonly Dictionary<int, Hotel> _hotels;

    public InMemoryHotelRepository()
    {
        _hotels = new Dictionary<int, Hotel>();
    }

    public void AddHotel(Hotel hotel)
    {
        if (_hotels.ContainsKey(hotel.Id))
        {
            throw new HotelAlreadyExistsException();
        }
        
        _hotels.Add(hotel.Id, hotel);
    }

    public Hotel GetHotel(int hotelId)
    {
        if (!_hotels.ContainsKey(hotelId))
        {
            throw new HotelNotFoundException();
        }
        
        return new Hotel(_hotels[hotelId].Id, _hotels[hotelId].Name);
    }
}