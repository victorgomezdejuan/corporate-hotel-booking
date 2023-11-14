using HotelManagement.Domain;
using HotelManagement.Repositories.Hotels;
using HotelManagement.Repositories.Rooms;

namespace HotelManagement.Application.Hotels.Commands.SetRoom;

public class SetRoomCommand
{
    public int HotelId { get; }

    public int RoomNumber { get; }

    public RoomType RoomType { get; }

    public SetRoomCommand(int hotelId, int roomNumber, RoomType roomType)
    {
        HotelId = hotelId;
        RoomNumber = roomNumber;
        RoomType = roomType;
    }
}

public class SetRoomCommandHandler
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomRepository _roomRepository;

    public SetRoomCommandHandler(IHotelRepository hotelRepository, IRoomRepository roomRepository)
    {
        _hotelRepository = hotelRepository;
        _roomRepository = roomRepository;
    }

    public void Handle(SetRoomCommand command)
    {
        var room = new Room(command.HotelId, command.RoomNumber, command.RoomType);
        if (_roomRepository.Exists(command.HotelId, command.RoomNumber))
        {
            _roomRepository.UpdateRoom(room);
        }
        else
        {
            if (!_hotelRepository.Exists(command.HotelId))
            {
                throw new HotelNotFoundException(command.HotelId);
            }

            _roomRepository.AddRoom(room);
        }
    }
}