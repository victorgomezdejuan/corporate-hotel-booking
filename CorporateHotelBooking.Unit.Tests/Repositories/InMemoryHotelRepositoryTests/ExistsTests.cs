using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryHotelRepositoryTests;

public class ExistsTests
{
    private readonly InMemoryHotelRepository _repository;

    public ExistsTests()
    {
        _repository = new InMemoryHotelRepository();
    }

    [Fact]
    public void HotelExists()
    {
        // Arrange
        var hotel = new Hotel(1, "Hotel");
        _repository.Add(hotel);

        // Act
        var exists = _repository.Exists(hotel.Id);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public void HotelDoesNotExist()
    {
        // Act
        var exists = _repository.Exists(1);

        // Assert
        Assert.False(exists);
    }
}