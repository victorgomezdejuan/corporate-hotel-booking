using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryHotelRepositoryTests;

public class GetTests
{
    [Fact]
    public void GetExistingHotel()
    {
        // Arrange
        var repository = new InMemoryHotelRepository();
        var addedHotel = new Hotel(1, "Hotel 1");
        repository.Add(addedHotel);
        
        // Act
        var retrievedHotel = repository.Get(1);
        
        // Assert
        Assert.Equal(addedHotel, retrievedHotel);
    }
}