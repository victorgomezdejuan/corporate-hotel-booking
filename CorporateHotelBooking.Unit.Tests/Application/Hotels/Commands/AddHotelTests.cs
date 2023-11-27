using CorporateHotelBooking.Application.Hotels.Commands.AddHotel;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.Hotels.Commands;

public class AddHotelTests
{
        private readonly Mock<IHotelRepository> _hotelRepositoryMock;
        private readonly AddHotelCommandHandler _handler;

        public AddHotelTests()
        {
            _hotelRepositoryMock = new Mock<IHotelRepository>();
            _handler = new AddHotelCommandHandler(_hotelRepositoryMock.Object);
        }

        [Fact]
        public void AddNewHotel()
        {
            // Arrange
            var command = new AddHotelCommand(1, "Hilton");

            // Act
            _handler.Handle(command);

            // Assert
            _hotelRepositoryMock.Verify(x => x.AddHotel(It.Is<Hotel>(h => h.Id == 1 && h.Name == "Hilton")));
        }

        [Fact]
        public void AddExistingHotel()
        {
            // Arrange
            _hotelRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
            var command = new AddHotelCommand(1, "Hilton");

            // Act
            void act() => _handler.Handle(command);

            // Assert
            Assert.Throws<HotelAlreadyExistsException>(act);
        }
}