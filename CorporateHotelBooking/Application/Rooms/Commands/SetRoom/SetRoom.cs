using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Application.Rooms.Commands.SetRoom;

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
        if (_roomRepository.ExistsRoomNumber(command.HotelId, command.RoomNumber))
        {
            _roomRepository.Update(room);
        }
        else
        {
            if (!_hotelRepository.Exists(command.HotelId))
            {
                throw new HotelNotFoundException(command.HotelId);
            }

            _roomRepository.Add(room);
        }
    }
}