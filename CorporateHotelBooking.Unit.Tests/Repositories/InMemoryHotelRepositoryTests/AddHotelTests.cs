using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryHotelRepositoryTests;

public class AddHotelTests
{
    private readonly InMemoryHotelRepository _repository;

    public AddHotelTests()
    {
        _repository = new InMemoryHotelRepository();
    }

    [Fact]
    public void AddNewHotel()
    {
        // Arrange
        var hotelToBeAdded = new Hotel(1, "Hotel 1");
        
        // Act
        _repository.AddHotel(hotelToBeAdded);
        
        // Assert
        var retrievedHotel = _repository.GetHotel(1);
        Assert.Equal(hotelToBeAdded, retrievedHotel);
    }
}