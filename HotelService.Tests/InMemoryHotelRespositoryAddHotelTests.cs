namespace HotelService.Tests;

public class InMemoryHotelRepositoryAddHotelTests
{
    private readonly InMemoryHotelRepository _repository;

    public InMemoryHotelRepositoryAddHotelTests()
    {
        _repository = new InMemoryHotelRepository();
    }

    [Fact]
    public void AddNewHotel()
    {
        // Arrange
        var hotel = new Hotel(1, "Hotel 1");
        
        // Act
        _repository.AddHotel(hotel);
        
        // Assert
        var actualHotel = _repository.GetHotel(1);
        Assert.Equal(hotel, actualHotel);
    }

    [Fact]
    public void AddExistingHotel()
    {
        // Arrange
        var hotel = new Hotel(1, "Hotel 1");
        _repository.AddHotel(hotel);
        
        // Act
        void act() => _repository.AddHotel(hotel);
        
        // Assert
        Assert.Throws<HotelAlreadyExistsException>(act);
    }
}