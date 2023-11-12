namespace HotelService;

public class Hotel
{
    public Hotel(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }
    public string Name { get; init; }
}