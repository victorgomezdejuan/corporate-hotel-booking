namespace HotelService;

public interface IHotelRepository
{
    void AddHotel(Hotel hotel);

    Hotel GetHotel(int hotelId);
}