namespace HotelService;

public enum RoomType
{
    Single,
    Double,
    Suite
}

public class HotelService
{
    private readonly IHotelRepository _hotelRepository;

    public HotelService(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public void AddHotel(int hotelId, string hotelName)
    {
        var hotel = new Hotel(hotelId, hotelName);
        _hotelRepository.AddHotel(hotel);
    }

    public void SetRoom(int hotelId, int number, RoomType roomType)
    {
        throw new NotImplementedException();
    }

    public HotelInfo FindHotel(int hotelId)
    {
        throw new NotImplementedException();
    }
}
