namespace HotelService.Tests;

public class RoomTests
{
    [Fact]
    public void UpdateRoom()
    {
        // Arrange
        var originalRoom = new Room(1, 100, RoomType.Single);
        
        // Act
        var updatedRoom = originalRoom.UpdateType(RoomType.Double);
        
        // Assert
        Assert.Equal(new Room(originalRoom.HotelId, originalRoom.Number, RoomType.Double), updatedRoom);
    }
}