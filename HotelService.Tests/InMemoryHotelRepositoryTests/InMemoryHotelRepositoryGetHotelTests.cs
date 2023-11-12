using HotelService.Domain;

namespace HotelService.Tests.InMemoryHotelRepositoryTests;

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
        var addedHotel = new Hotel(1, "Hotel 1");
        _repository.AddHotel(addedHotel);
        
        // Act
        var retrievedHotel = _repository.GetHotel(1);
        
        // Assert
        Assert.Equal(addedHotel, retrievedHotel);
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