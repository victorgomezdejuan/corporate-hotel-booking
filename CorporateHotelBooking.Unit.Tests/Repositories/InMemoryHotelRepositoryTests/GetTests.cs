using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryHotelRepositoryTests;

public class GetTests
{
    [Theory, AutoData]
    public void GetExistingHotel(Hotel hotel)
    {
        // Arrange
        var repository = new InMemoryHotelRepository();
        repository.Add(hotel);
        
        // Act
        var retrievedHotel = repository.Get(hotel.Id);
        
        // Assert
        retrievedHotel.Should().Be(hotel);
    }
}