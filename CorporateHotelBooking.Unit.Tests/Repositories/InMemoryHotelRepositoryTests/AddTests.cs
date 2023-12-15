using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryHotelRepositoryTests;

public class AddTests
{
    [Fact]
    public void AddNewHotel()
    {
        // Arrange
        var repository = new InMemoryHotelRepository();
        var hotelToBeAdded = new Hotel(1, "Hotel 1");
        
        // Act
        repository.Add(hotelToBeAdded);
        
        // Assert
        var retrievedHotel = repository.Get(1);
        Assert.Equal(hotelToBeAdded, retrievedHotel);
    }
}