namespace HotelService;

public class HotelRepository : IHotelRepository
{
    private readonly Dictionary<int, Hotel> _hotels;

    public HotelRepository()
    {
        _hotels = new Dictionary<int, Hotel>();
    }

    public void AddHotel(Hotel hotel)
    {
        _hotels.Add(hotel.Id, hotel);
    }

    public Hotel GetHotel(int hotelId)
    {
        return new Hotel(_hotels[hotelId].Id, _hotels[hotelId].Name);
    }
}