using HotelManagement.Domain;
using HotelManagement.Repositories;

namespace HotelManagement.Unit.Tests.InMemoryHotelRepositoryTests;

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
        var hotelToBeAdded = new Hotel(1, "Hotel 1");
        
        // Act
        _repository.AddHotel(hotelToBeAdded);
        
        // Assert
        var retrievedHotel = _repository.GetHotel(1);
        Assert.Equal(hotelToBeAdded, retrievedHotel);
    }
}