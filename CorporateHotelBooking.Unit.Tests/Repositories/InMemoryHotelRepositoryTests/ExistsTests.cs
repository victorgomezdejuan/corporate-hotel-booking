using AutoFixture.Xunit2;
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

    [Theory, AutoData]
    public void HotelExists(Hotel hotel)
    {
        // Arrange
        _repository.Add(hotel);

        // Act
        var exists = _repository.Exists(hotel.Id);

        // Assert
        Assert.True(exists);
    }

    [Theory, AutoData]
    public void HotelDoesNotExist(int hotelId)
    {
        // Act
        var exists = _repository.Exists(hotelId);

        // Assert
        Assert.False(exists);
    }
}