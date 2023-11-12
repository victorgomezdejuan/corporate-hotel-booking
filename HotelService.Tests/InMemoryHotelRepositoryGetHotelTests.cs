namespace HotelService.Tests;

public class InMemoryHotelRepositoryGetHotelTests
{
    private readonly InMemoryHotelRepository _repository;
    
    public InMemoryHotelRepositoryGetHotelTests()
    {
        _repository = new InMemoryHotelRepository();
    }
    
    [Fact]
    public void GetExistingHotel()
    {
        // Arrange
        var hotel = new Hotel(1, "Hotel 1");
        _repository.AddHotel(hotel);
        
        // Act
        var actualHotel = _repository.GetHotel(1);
        
        // Assert
        Assert.Equal(hotel, actualHotel);
    }

    [Fact]
    public void GetNonExistingHotel()
    {
       
        // Act
        void act() => _repository.GetHotel(1);
        
        // Assert
        Assert.Throws<HotelNotFoundException>(act);
    }
}