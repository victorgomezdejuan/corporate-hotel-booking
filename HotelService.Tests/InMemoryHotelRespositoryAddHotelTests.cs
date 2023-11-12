namespace HotelService.Tests;

public class InMemoryHotelRepositoryAddHotelTests
{
    [Fact]
    public void AddNewHotel()
    {
        // Arrange
        var hotel = new Hotel(1, "Hotel 1");
        var repository = new InMemoryHotelRepository();
        
        // Act
        repository.AddHotel(hotel);
        
        // Assert
        var actualHotel = repository.GetHotel(1);
        Assert.Equal(hotel, actualHotel);
    }

    [Fact]
    public void AddExistingHotel()
    {
        // Arrange
        var hotel = new Hotel(1, "Hotel 1");
        var repository = new InMemoryHotelRepository();
        repository.AddHotel(hotel);
        
        // Act
        void act() => repository.AddHotel(hotel);
        
        // Assert
        Assert.Throws<HotelAlreadyExistsException>(act);
    }
}