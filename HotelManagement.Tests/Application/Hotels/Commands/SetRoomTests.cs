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
        var roomRepository = new Mock<IRoomRepository>();
        var setRoomCommandHandler = new SetRoomCommandHandler(roomRepository.Object);
        var setRoomCommand = new SetRoomCommand(1, 100, RoomType.Single);

        // Act
        setRoomCommandHandler.Handle(setRoomCommand);

        // Assert
        roomRepository.Verify(x => x.AddRoom(It.Is<Room>(r => r.Equals(new Room(1, 100, RoomType.Single)))));
    }
}