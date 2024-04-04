using CorporateHotelBooking.Domain.ValueObjects;

namespace CorporateHotelBooking.Domain.Entities;

public record Booking
{
    public Booking(int employeeId, int hotelId, RoomType roomType, BookingDateRange dateRange)
    {
        EmployeeId = employeeId;
        HotelId = hotelId;
        RoomType = roomType;
        DateRange = dateRange;
    }

    public Booking(int id, int employeeId, int hotelId, RoomType roomType, BookingDateRange dateRange) 
        : this(employeeId, hotelId, roomType, dateRange)
    {
        Id = id;
    }

    public int? Id { get; }
    public int EmployeeId { get; }
    public int HotelId { get; }
    public RoomType RoomType { get; }
    public BookingDateRange DateRange { get; }
}