using CorporateHotelBooking.Application.Rooms.Commands.SetRoom;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.Rooms.Commands;

public class SetRoomTests
{
    private readonly SetRoomCommand _setRoomCommand;
    private readonly Mock<IRoomRepository> _roomRepositoryMock;
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly SetRoomCommandHandler _setRoomCommandHandler;

    public SetRoomTests()
    {
        _setRoomCommand = new SetRoomCommand(1, 100, RoomType.Standard);
        _roomRepositoryMock = new Mock<IRoomRepository>();
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _setRoomCommandHandler = new SetRoomCommandHandler(_hotelRepositoryMock.Object, _roomRepositoryMock.Object);
    }

    [Fact]
    public void AddNewRoom()
    {
        // Arrange
        _roomRepositoryMock.Setup(x => x.ExistsRoomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
        _hotelRepositoryMock.Setup(x => x.Exists(_setRoomCommand.HotelId)).Returns(true);

        // Act
        _setRoomCommandHandler.Handle(_setRoomCommand);

        // Assert
        _roomRepositoryMock.Verify(x => x.Add(It.Is<Room>(r => r.Equals(new Room(1, 100, RoomType.Standard)))));
    }

    [Fact]
    public void UpdateExistingRoom()
    {
        // Arrange
        _roomRepositoryMock.Setup(x => x.ExistsRoomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
        _hotelRepositoryMock.Setup(x => x.Exists(_setRoomCommand.HotelId)).Returns(true);

        // Act
        _setRoomCommandHandler.Handle(_setRoomCommand);

        // Assert
        _roomRepositoryMock.Verify(x => x.Update(It.Is<Room>(r => r.Equals(new Room(1, 100, RoomType.Standard)))));
    }

    [Fact]
    public void SetRoomFromNonExistingHotel()
    {
        // Arrange
        _roomRepositoryMock.Setup(x => x.ExistsRoomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
        _hotelRepositoryMock.Setup(x => x.Exists(_setRoomCommand.HotelId)).Returns(false);

        // Act
        void act() => _setRoomCommandHandler.Handle(_setRoomCommand);

        // Assert
        Assert.Throws<HotelNotFoundException>(act);
    }
}