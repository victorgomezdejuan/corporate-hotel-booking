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
    private readonly IRoomRepository _roomRepository;

    public HotelService(IHotelRepository hotelRepository, IRoomRepository roomRepository)
    {
        _hotelRepository = hotelRepository;
        _roomRepository = roomRepository;
    }

    public void AddHotel(int hotelId, string hotelName)
    {
        var hotel = new Hotel(hotelId, hotelName);
        _hotelRepository.AddHotel(hotel);
    }

    public void SetRoom(int hotelId, int number, RoomType roomType)
    {
        if (!_hotelRepository.Exists(hotelId))
        {
            throw new HotelNotFoundException();
        }
        
        var room = new Room(hotelId, number, roomType);
        _roomRepository.AddRoom(room);
    }

    public HotelInfo FindHotel(int hotelId)
    {
        throw new NotImplementedException();
    }
}
