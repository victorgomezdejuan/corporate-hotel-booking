using Moq;

namespace HotelService.Tests;

public class HotelServiceAddHotelTests
{
    [Fact]
    public void AddNewHotel()
    {
        // Arrange
        var hotelRepository = new Mock<IHotelRepository>();
        var hotelService = new HotelService(hotelRepository.Object);

        // Act
        hotelService.AddHotel(1, "Hilton");

        // Assert
        hotelRepository.Verify(x => x.AddHotel(It.Is<Hotel>(h => h.Id == 1 && h.Name == "Hilton")));
    }

    [Fact]
    public void AddExistingHotel()
    {
        // Arrange
        var hotelRepository = new Mock<IHotelRepository>();
        hotelRepository.Setup(x => x.AddHotel(It.IsAny<Hotel>())).Throws<HotelAlreadyExistsException>();
        var hotelService = new HotelService(hotelRepository.Object);

        // Act
        void act() => hotelService.AddHotel(1, "Hilton");

        // Assert
        Assert.Throws<HotelAlreadyExistsException>(act);
    }
}