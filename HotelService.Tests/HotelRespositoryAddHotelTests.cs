namespace HotelService.Tests;

public class HotelRespositoryAddHotelTests
{
    [Fact]
    public void AddNewHotel()
    {
        // Arrange
        var hotel = new Hotel(1, "Hotel 1");
        var repository = new HotelRepository();
        
        // Act
        repository.AddHotel(hotel);
        
        // Assert
        var actualHotel = repository.GetHotel(1);
        Assert.Equal(hotel, actualHotel);
    }
}