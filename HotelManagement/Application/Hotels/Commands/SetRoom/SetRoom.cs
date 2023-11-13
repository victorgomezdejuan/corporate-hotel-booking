using HotelManagement.Domain;
using HotelManagement.Repositories;

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
    private readonly IRoomRepository _roomRepository;

    public SetRoomCommandHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public void Handle(SetRoomCommand command)
    {
        _roomRepository.AddRoom(new Room(command.HotelId, command.RoomNumber, command.RoomType));
    }
}