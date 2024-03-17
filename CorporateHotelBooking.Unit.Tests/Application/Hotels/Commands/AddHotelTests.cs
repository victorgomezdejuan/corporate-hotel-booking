using AutoFixture.Xunit2;
using CorporateHotelBooking.Application.Hotels.Commands.AddHotel;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;
using FluentAssertions;
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

        [Theory, AutoData]
        public void AddNewHotel(int hotelId, string hotelName)
        {
            // Arrange
            var command = new AddHotelCommand(hotelId, hotelName);

            // Act
            _handler.Handle(command);

            // Assert
            _hotelRepositoryMock.Verify(x => x.Add(It.Is<Hotel>(h => h.Id == hotelId && h.Name == hotelName)));
        }

        [Theory, AutoData]
        public void AddExistingHotel(int hotelId, string hotelName)
        {
            // Arrange
            _hotelRepositoryMock.Setup(x => x.Exists(hotelId)).Returns(true);
            var command = new AddHotelCommand(hotelId, hotelName);

            // Act
            var result = _handler.Handle(command);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("Hotel already exists");
        }
}