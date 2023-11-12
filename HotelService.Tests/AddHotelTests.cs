using Moq;

namespace HotelService.Tests;

public class AddHotelTests
{
    [Fact]
    public void AddNewHotel()
    {
        var hotelRepository = new Mock<IHotelRepository>();
        var hotelService = new HotelService(hotelRepository.Object);

        hotelService.AddHotel(1, "Hilton");

        hotelRepository.Verify(x => x.AddHotel(It.Is<Hotel>(h => h.Id == 1 && h.Name == "Hilton")));
    }

    [Fact]
    public void AddExistingHotel()
    {
        var hotelRepository = new Mock<IHotelRepository>();
        hotelRepository.Setup(x => x.AddHotel(It.IsAny<Hotel>())).Throws<HotelAlreadyExistsException>();
        var hotelService = new HotelService(hotelRepository.Object);

        void act() => hotelService.AddHotel(1, "Hilton");

        Assert.Throws<HotelAlreadyExistsException>(act);
    }
}