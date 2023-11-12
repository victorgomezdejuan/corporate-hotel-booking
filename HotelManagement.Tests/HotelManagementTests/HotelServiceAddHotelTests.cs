using HotelManagement.Domain;
using HotelManagement.Repositories;
using HotelManagement.Service;
using Moq;

namespace HotelManagement.Tests.HotelServiceTests;

public class HotelServiceAddHotelTests
{
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly HotelService _hotelService;

    public HotelServiceAddHotelTests()
    {
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        // CODE SMELL: We have to pass null as the second parameter because we don't need it in this test
        _hotelService = new HotelService(_hotelRepositoryMock.Object, null);
    }

    [Fact]
    public void AddNewHotel()
    {
        // Act
        _hotelService.AddHotel(1, "Hilton");

        // Assert
        _hotelRepositoryMock.Verify(x => x.AddHotel(It.Is<Hotel>(h => h.Id == 1 && h.Name == "Hilton")));
    }

    [Fact]
    public void AddExistingHotel()
    {
        // Arrange
        _hotelRepositoryMock.Setup(x => x.AddHotel(It.IsAny<Hotel>())).Throws<HotelAlreadyExistsException>();

        // Act
        void act() => _hotelService.AddHotel(1, "Hilton");

        // Assert
        Assert.Throws<HotelAlreadyExistsException>(act);
    }
}