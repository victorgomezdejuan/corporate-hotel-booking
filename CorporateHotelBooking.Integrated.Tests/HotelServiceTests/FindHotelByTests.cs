using FluentAssertions;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;
using CorporateHotelBooking.Services;
using AutoFixture.Xunit2;

namespace CorporateHotelBooking.Integrated.Tests.HotelServiceTests;

public class FindHotelByTests
{
    [Theory, AutoData]
    public void FindAHotel(int hotelId, string hotelName,
        int room1Number, int room2Number, RoomType room1Type, RoomType room2Type)
    {
        // Arrange
        var hotelRepository = new InMemoryHotelRepository();
        var roomRepository = new InMemoryRoomRepository();
        var hotelService = new HotelService(hotelRepository, roomRepository);
        hotelService.AddHotel(hotelId, hotelName);
        hotelService.SetRoom(hotelId, room1Number, room1Type);
        hotelService.SetRoom(hotelId, room2Number, room2Type);

        // Act
        var hotel = hotelService.FindHotelBy(hotelId)!;

        // Assert
        hotel.Id.Should().Be(hotelId);
        hotel.Name.Should().Be(hotelName);
        hotel.Rooms.Should().HaveCount(2);
        hotel.Rooms.Should().Contain(r => r.Number == room1Number && r.Type == room1Type);
        hotel.Rooms.Should().Contain(r => r.Number == room2Number && r.Type == room2Type);
    }
}