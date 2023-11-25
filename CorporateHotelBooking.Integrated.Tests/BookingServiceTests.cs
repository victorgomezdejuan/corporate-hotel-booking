using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests;

public class BookingServiceTests
{
    [Fact]
    public void BookARoom()
    {
        // // Arrange
        // var expectedBooking = new Booking
        // (
        //     id: 1,
        //     employeeId: 2,
        //     hotelId: 3,
        //     roomType: RoomType.Standard,
        //     checkInDate: DateOnly.FromDateTime(DateTime.Now),
        //     checkOutDate: DateOnly.FromDateTime(DateTime.Now.AddDays(1))
        // );
        // IBookingRepository bookingRepository = new InMemoryBookingRepository();
        // IHotelRepository hotelRepository = new InMemoryHotelRepository();
        // hotelRepository.AddHotel(new Hotel(3, "Hotel 3"));
        // IRoomRepository roomRepository = new InMemoryRoomRepository();
        // roomRepository.AddRoom(new Room(3, 100, RoomType.Standard));
        // var bookingService = new BookingService(bookingRepository, hotelRepository, roomRepository);

        // // Act
        // var booking = bookingService.Book(2, 3, RoomType.Standard, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1)));

        // // Assert
        // booking.Should().Be(expectedBooking);
        // bookingRepository.GetBooking(1).Should().BeEquivalentTo(expectedBooking);
    }
}