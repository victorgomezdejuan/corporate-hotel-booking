using CorporateHotelBooking.Application.Common;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Application.Rooms.Commands.SetRoom;

public record SetRoomCommand(int HotelId, int RoomNumber, RoomType RoomType);

public class SetRoomCommandHandler
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomRepository _roomRepository;

    public SetRoomCommandHandler(IHotelRepository hotelRepository, IRoomRepository roomRepository)
    {
        _hotelRepository = hotelRepository;
        _roomRepository = roomRepository;
    }

    public Result Handle(SetRoomCommand command)
    {
        var room = new Room(command.HotelId, command.RoomNumber, command.RoomType);
        
        if (_roomRepository.ExistsRoomNumber(command.HotelId, command.RoomNumber))
        {
            return UpdateExistingRoom(room);
        }
        else
        {
            return AddNewRoom(command.HotelId, room);
        }
    }

    private Result UpdateExistingRoom(Room room)
    {
        _roomRepository.Update(room);

        return Result.Success();
    }

    private Result AddNewRoom(int hotelId, Room room)
    {
        if (!_hotelRepository.Exists(hotelId))
        {
            return Result.Failure("Hotel does not exist");
        }

        _roomRepository.Add(room);

        return Result.Success();
    }
}