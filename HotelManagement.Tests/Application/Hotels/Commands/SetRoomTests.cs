using HotelManagement.Application.Hotels.Commands.SetRoom;
using HotelManagement.Domain;
using HotelManagement.Repositories;
using Moq;

namespace HotelManagement.Tests.Application.Hotels.Commands;

public class SetRoomTests
{
    [Fact]
    public void AddNewRoom()
    {
        // Arrange
        var setRoomCommand = new SetRoomCommand(1, 100, RoomType.Single);
        var roomRepository = new Mock<IRoomRepository>();
        roomRepository.Setup(x => x.Exists(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
        var hotelRepositoryMock = new Mock<IHotelRepository>();
        hotelRepositoryMock.Setup(x => x.Exists(setRoomCommand.HotelId)).Returns(true);
        var setRoomCommandHandler = new SetRoomCommandHandler(hotelRepositoryMock.Object, roomRepository.Object);

        // Act
        setRoomCommandHandler.Handle(setRoomCommand);

        // Assert
        roomRepository.Verify(x => x.AddRoom(It.Is<Room>(r => r.Equals(new Room(1, 100, RoomType.Single)))));
    }

    [Fact]
    public void UpdateExistingRoom()
    {
        // Arrange
        var setRoomCommand = new SetRoomCommand(1, 100, RoomType.Single);
        var roomRepository = new Mock<IRoomRepository>();
        roomRepository.Setup(x => x.Exists(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
        var hotelRepositoryMock = new Mock<IHotelRepository>();
        hotelRepositoryMock.Setup(x => x.Exists(setRoomCommand.HotelId)).Returns(true);
        var setRoomCommandHandler = new SetRoomCommandHandler(hotelRepositoryMock.Object, roomRepository.Object);

        // Act
        setRoomCommandHandler.Handle(setRoomCommand);

        // Assert
        roomRepository.Verify(x => x.UpdateRoom(It.Is<Room>(r => r.Equals(new Room(1, 100, RoomType.Single)))));
    }

    [Fact]
    public void SetRoomFromNonExistingHotel()
    {
        // Arrange
        var setRoomCommand = new SetRoomCommand(1, 100, RoomType.Single);
        var roomRepository = new Mock<IRoomRepository>();
        roomRepository.Setup(x => x.Exists(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
        var hotelRepositoryMock = new Mock<IHotelRepository>();
        hotelRepositoryMock.Setup(x => x.Exists(setRoomCommand.HotelId)).Returns(false);
        var setRoomCommandHandler = new SetRoomCommandHandler(hotelRepositoryMock.Object, roomRepository.Object);

        // Act
        void act() => setRoomCommandHandler.Handle(setRoomCommand);

        // Assert
        Assert.Throws<HotelNotFoundException>(act);
    }
}