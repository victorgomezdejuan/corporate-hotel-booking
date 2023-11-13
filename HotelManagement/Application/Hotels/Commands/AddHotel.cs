using HotelManagement.Application;
using HotelManagement.Domain;
using HotelManagement.Repositories;

namespace HotelManagement.Application.Hotels.Commands.AddHotel;

public record AddHotelCommand
{
    public int HotelId { get; }
    public string HotelName { get; }

    public AddHotelCommand(int hotelId, string hotelName)
    {
        HotelId = hotelId;
        HotelName = hotelName;
    }
}

public class AddHotelCommandHandler
{
    private readonly IHotelRepository _hotelRepository;

    public AddHotelCommandHandler(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public void Handle(AddHotelCommand command)
    {
        if (_hotelRepository.Exists(command.HotelId))
        {
            throw new HotelAlreadyExistsException();
        }
        
        var hotel = new Hotel(command.HotelId, command.HotelName);
        _hotelRepository.AddHotel(hotel);
    }
}