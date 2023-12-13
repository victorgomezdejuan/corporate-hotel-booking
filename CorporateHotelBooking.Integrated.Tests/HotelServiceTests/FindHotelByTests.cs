using FluentAssertions;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;
using CorporateHotelBooking.Services;

namespace CorporateHotelBooking.Integrated.Tests.HotelServiceTests;

public class FindHotelByTests
{
    [Fact]
    public void FindAHotel()
    {
        // Arrange
        var hotelRepository = new InMemoryHotelRepository();
        var roomRepository = new InMemoryRoomRepository();
        var hotelService = new HotelService(hotelRepository, roomRepository);
        hotelService.AddHotel(1, "Hotel 1");
        hotelService.SetRoom(1, 101, RoomType.Standard);
        hotelService.SetRoom(1, 102, RoomType.JuniorSuite);

        // Act
        var hotel = hotelService.FindHotelBy(1);

        // Assert
        hotel.Id.Should().Be(1);
        hotel.Name.Should().Be("Hotel 1");
        hotel.Rooms.Should().HaveCount(2);
        hotel.Rooms.Should().Contain(r => r.Number == 101 && r.Type == RoomType.Standard);
        hotel.Rooms.Should().Contain(r => r.Number == 102 && r.Type == RoomType.JuniorSuite);
    }
}