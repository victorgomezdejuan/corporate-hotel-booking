using AutoFixture;
using AutoFixture.Xunit2;
using CorporateHotelBooking.Application.Rooms.Commands.SetRoom;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.Rooms.Commands;

public class SetRoomTests
{
    private readonly SetRoomCommand _command;
    private readonly Mock<IRoomRepository> _roomRepositoryMock;
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly SetRoomCommandHandler _setRoomCommandHandler;

    public SetRoomTests()
    {
        var fixture = new Fixture();
        _command = new SetRoomCommand(fixture.Create<int>(), fixture.Create<int>(), fixture.Create<RoomType>());
        _roomRepositoryMock = new Mock<IRoomRepository>();
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _setRoomCommandHandler = new SetRoomCommandHandler(_hotelRepositoryMock.Object, _roomRepositoryMock.Object);
    }

    [Fact]
    public void AddNewRoom()
    {
        // Arrange
        _roomRepositoryMock.Setup(x => x.ExistsRoomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
        _hotelRepositoryMock.Setup(x => x.Exists(_command.HotelId)).Returns(true);

        // Act
        _setRoomCommandHandler.Handle(_command);

        // Assert
        _roomRepositoryMock
        .Verify(x => x.Add(It.Is<Room>(r => r.Equals(
            new Room(_command.HotelId, _command.RoomNumber, _command.RoomType)))));
    }

    [Fact]
    public void UpdateExistingRoom()
    {
        // Arrange
        _roomRepositoryMock.Setup(x => x.ExistsRoomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
        _hotelRepositoryMock.Setup(x => x.Exists(_command.HotelId)).Returns(true);

        // Act
        _setRoomCommandHandler.Handle(_command);

        // Assert
        _roomRepositoryMock.Verify(x => x.Update(It.Is<Room>(r => r.Equals(
            new Room(_command.HotelId, _command.RoomNumber, _command.RoomType)))));
    }

    [Fact]
    public void SetRoomFromNonExistingHotel()
    {
        // Arrange
        _roomRepositoryMock.Setup(x => x.ExistsRoomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
        _hotelRepositoryMock.Setup(x => x.Exists(_command.HotelId)).Returns(false);

        // Act
        void act() => _setRoomCommandHandler.Handle(_command);

        // Assert
        Assert.Throws<HotelNotFoundException>(act);
    }
}