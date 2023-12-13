using CorporateHotelBooking.Domain.Entities;

namespace CorporateHotelBooking.Application.Bookings.Commands;

public record NewBooking
(
    int Id,
    int EmployeeId,
    int HotelId,
    RoomType RoomType,
    DateOnly CheckInDate,
    DateOnly CheckOutDate
);