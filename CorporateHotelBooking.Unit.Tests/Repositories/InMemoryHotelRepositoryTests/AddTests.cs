using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;
using FluentAssertions;

namespace CorporateHotelBooking.Unit.Tests.Repositories.InMemoryHotelRepositoryTests;

public class AddTests
{
    [Theory, AutoData]
    public void AddNewHotel(Hotel hotel)
    {
        // Arrange
        var repository = new InMemoryHotelRepository();
        
        // Act
        repository.Add(hotel);
        
        // Assert
        var retrievedHotel = repository.Get(hotel.Id);
        retrievedHotel.Should().Be(hotel);
    }
}