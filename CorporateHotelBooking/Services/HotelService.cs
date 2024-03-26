using CorporateHotelBooking.Application;
using CorporateHotelBooking.Application.Common;
using CorporateHotelBooking.Application.Hotels.Commands.AddHotel;
using CorporateHotelBooking.Application.Hotels.Queries.FindHotel;
using CorporateHotelBooking.Application.Rooms.Commands.SetRoom;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Services;

public class HotelService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomRepository _roomRepository;

    public HotelService(IHotelRepository hotelRepository, IRoomRepository roomRepository)
    {
        _hotelRepository = hotelRepository;
        _roomRepository = roomRepository;
    }

    public Result AddHotel(int hotelId, string hotelName)
    {
        return new AddHotelCommandHandler(_hotelRepository).Handle(new AddHotelCommand(hotelId, hotelName));
    }

    public Result SetRoom(int hotelId, int number, RoomType roomType)
    {
        return new SetRoomCommandHandler(_hotelRepository, _roomRepository).Handle(new SetRoomCommand(hotelId, number, roomType));
    }

    public HotelDto? FindHotelBy(int hotelId)
    {
        return new FindHotelQueryHandler(_hotelRepository, _roomRepository).Handle(new FindHotelQuery(hotelId));
    }
}
