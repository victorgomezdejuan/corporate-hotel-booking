using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;

namespace CorporateHotelBooking.Application.Hotels.Commands.AddHotel;

public record AddHotelCommand(int HotelId, string HotelName);

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
        _hotelRepository.Add(new Hotel(command.HotelId, command.HotelName));
    }
}