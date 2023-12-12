using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryHotelRepositoryTests;

public class GetHotelTests
{
    private readonly InMemoryHotelRepository _repository;
    
    public GetHotelTests()
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
}