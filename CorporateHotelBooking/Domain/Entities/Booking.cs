using CorporateHotelBooking.Domain.Exceptions;

namespace CorporateHotelBooking.Domain.Entities;

public class Booking
{
    public Booking(int employeeId, int hotelId, RoomType roomType, DateOnly checkInDate, DateOnly checkOutDate)
    {
        if (checkOutDate <= checkInDate)
            throw new CheckOutDateMustBeAfterCheckInDateException();

        EmployeeId = employeeId;
        HotelId = hotelId;
        RoomType = roomType;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
    }

    public Booking(int id, int employeeId, int hotelId, RoomType roomType, DateOnly checkInDate, DateOnly checkOutDate) 
        : this(employeeId, hotelId, roomType, checkInDate, checkOutDate)
    {
        Id = id;
    }

    public int? Id { get; }
    public int EmployeeId { get; }
    public int HotelId { get; }
    public RoomType RoomType { get; }
    public DateOnly CheckInDate { get; }
    public DateOnly CheckOutDate { get; }
}