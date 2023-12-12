using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryHotelRepositoryTests;

public class GetTests
{
    private readonly InMemoryHotelRepository _repository;
    
    public GetTests()
    {
        _repository = new InMemoryHotelRepository();
    }
    
    [Fact]
    public void GetExistingHotel()
    {
        // Arrange
        var addedHotel = new Hotel(1, "Hotel 1");
        _repository.Add(addedHotel);
        
        // Act
        var retrievedHotel = _repository.Get(1);
        
        // Assert
        Assert.Equal(addedHotel, retrievedHotel);
    }
}