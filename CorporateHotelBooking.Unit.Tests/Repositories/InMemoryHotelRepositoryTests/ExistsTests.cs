using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryHotelRepositoryTests;

public class ExistsTests
{
    private readonly InMemoryHotelRepository _hotelRepository;

    public ExistsTests()
    {
        _hotelRepository = new InMemoryHotelRepository();
    }

    [Fact]
    public void HotelExists()
    {
        // Arrange
        var hotel = new Hotel(1, "Hotel");
        _hotelRepository.Add(hotel);

        // Act
        var exists = _hotelRepository.Exists(hotel.Id);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public void HotelDoesNotExist()
    {
        // Act
        var exists = _hotelRepository.Exists(1);

        // Assert
        Assert.False(exists);
    }
}