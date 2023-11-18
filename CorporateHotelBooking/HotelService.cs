﻿using HotelManagement.Application;
using HotelManagement.Application.Hotels.Commands.AddHotel;
using HotelManagement.Application.Hotels.Queries.FindHotel;
using HotelManagement.Application.Rooms.Commands.SetRoom;
using HotelManagement.Domain;
using HotelManagement.Repositories.Hotels;
using HotelManagement.Repositories.Rooms;

namespace HotelManagement;

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
        new SetRoomCommandHandler(_hotelRepository, _roomRepository).Handle(new SetRoomCommand(hotelId, number, roomType));
    }

    public HotelDto FindHotelBy(int hotelId)
    {
        return new FindHotelQueryHandler(_hotelRepository, _roomRepository).Handle(new FindHotelQuery(hotelId));
    }
}