using HotelManagement.Application.Hotels.Commands.AddHotel;
using HotelManagement.Domain;
using HotelManagement.Repositories;

namespace HotelManagement.Application;

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
        new AddHotelCommandHandler(_hotelRepository).Handle(new AddHotelCommand(hotelId, hotelName));
    }

    public void SetRoom(int hotelId, int number, RoomType roomType)
    {
        if (!_hotelRepository.Exists(hotelId))
        {
            throw new HotelNotFoundException();
        }

        if (_roomRepository.Exists(hotelId, number))
        {
            var room = _roomRepository.GetRoom(hotelId, number);
            _roomRepository.UpdateRoom(room.UpdateType(roomType));
        }
        else
        {
            var room = new Room(hotelId, number, roomType);
            _roomRepository.AddRoom(room);
        }
    }

    public HotelInfo FindHotel(int hotelId)
    {
        throw new NotImplementedException();
    }
}
