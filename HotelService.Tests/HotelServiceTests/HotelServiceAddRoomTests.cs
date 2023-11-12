using Moq;

namespace HotelService.Tests.HotelServiceTests;

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
}