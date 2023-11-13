using HotelManagement.Domain;
using HotelManagement.Repositories;
using HotelManagement.Service;
using Moq;

namespace HotelManagement.Tests.HotelServiceTests;

public class HotelServiceAddRoomTests
{
    [Fact]
    public void AddNewRoom()
    {
        // Arrange
        var roomToBeAdded = new Room(1, 100, RoomType.Single);
        var hotelRepositoryMock = new Mock<IHotelRepository>();
        var roomRepositoryMock = new Mock<IRoomRepository>();
        var hotelService = new HotelService(hotelRepositoryMock.Object, roomRepositoryMock.Object);
        hotelRepositoryMock.Setup(x => x.Exists(roomToBeAdded.HotelId)).Returns(true);
        
        // Act
        hotelService.SetRoom(roomToBeAdded.HotelId, roomToBeAdded.Number, roomToBeAdded.Type);
        
        // Assert
        roomRepositoryMock.Verify(x => x.AddRoom(It.Is<Room>(r => r.Equals(roomToBeAdded))));
    }

    [Fact]
    public void SetRoomToNonExistingHotel()
    {
        // Arrange
        var hotelRepositoryMock = new Mock<IHotelRepository>();
        var roomRepositoryMock = new Mock<IRoomRepository>();
        var hotelService = new HotelService(hotelRepositoryMock.Object, roomRepositoryMock.Object);
        hotelRepositoryMock.Setup(x => x.Exists(1)).Returns(false);
        
        // Act
        void act() => hotelService.SetRoom(1, 100, RoomType.Single);
        
        // Assert
        Assert.Throws<HotelNotFoundException>(act);
    }

    [Fact]
    public void SetExistingRoomToExistingHotel()
    {
        // Arrange
        var originalExistingRoom = new Room(1, 100, RoomType.Single);
        var updatedRoom = new Room(1, 100, RoomType.Double);
        var hotelRepositoryMock = new Mock<IHotelRepository>();
        var roomRepositoryMock = new Mock<IRoomRepository>();
        var hotelService = new HotelService(hotelRepositoryMock.Object, roomRepositoryMock.Object);
        hotelRepositoryMock.Setup(x => x.Exists(originalExistingRoom.HotelId)).Returns(true);
        roomRepositoryMock.Setup(x => x.Exists(originalExistingRoom.HotelId, originalExistingRoom.Number)).Returns(true);
        roomRepositoryMock.Setup(x => x.GetRoom(originalExistingRoom.HotelId, originalExistingRoom.Number)).Returns(originalExistingRoom);
        
        // Act
        hotelService.SetRoom(updatedRoom.HotelId, updatedRoom.Number, updatedRoom.Type);
        
        // Assert
        roomRepositoryMock.Verify(x => x.UpdateRoom(It.Is<Room>(r => r.Equals(updatedRoom))));
    }
}