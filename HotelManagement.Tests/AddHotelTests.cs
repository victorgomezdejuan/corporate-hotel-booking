using HotelManagement.Domain;
using HotelManagement.Repositories;
using Moq;

namespace HotelManagement.Tests;

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
}