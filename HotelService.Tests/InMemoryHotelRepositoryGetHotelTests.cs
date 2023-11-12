namespace HotelService.Tests;

public class InMemoryHotelRepositoryGetHotelTests
{
    [Fact]
    public void GetExistingHotel()
    {
        // Arrange
        var hotel = new Hotel(1, "Hotel 1");
        var repository = new InMemoryHotelRepository();
        repository.AddHotel(hotel);
        
        // Act
        var actualHotel = repository.GetHotel(1);
        
        // Assert
        Assert.Equal(hotel, actualHotel);
    }

    [Fact]
    public void GetNonExistingHotel()
    {
        // Arrange
        var repository = new InMemoryHotelRepository();
        
        // Act
        void act() => repository.GetHotel(1);
        
        // Assert
        Assert.Throws<HotelNotFoundException>(act);
    }
}