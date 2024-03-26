using AutoFixture.Xunit2;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;
using CorporateHotelBooking.Services;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.HotelServiceTests;

public class AddHotelTests
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly HotelService _hotelService;

    public AddHotelTests()
    {
        _hotelRepository = new InMemoryHotelRepository();
        _roomRepository = new InMemoryRoomRepository();
        _hotelService = new HotelService(_hotelRepository, _roomRepository);
    }

    [Theory, AutoData]
    public void AddNewHotel(int hotelId, string hotelName)
    {
        // Act
        var result = _hotelService.AddHotel(hotelId, hotelName);

        // Assert
        result.IsFailure.Should().BeFalse();
        var hotel = _hotelService.FindHotelBy(hotelId)!;
        hotel.Id.Should().Be(hotelId);
        hotel.Name.Should().Be(hotelName);
    }

    [Theory, AutoData]
    public void AddHotelWithExistingHotelId(int hotelId)
    {
        // Arrange
        _hotelService.AddHotel(hotelId, "One name");

        // Act
        var result = _hotelService.AddHotel(hotelId, "Another name");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Hotel already exists");
    }
}