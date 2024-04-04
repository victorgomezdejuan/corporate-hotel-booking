using CorporateHotelBooking.Domain.Exceptions;

namespace CorporateHotelBooking.Domain.ValueObjects;

public readonly record struct BookingDateRange
{
    public BookingDateRange(DateOnly checkInDate, DateOnly checkOutDate)
    {
        if (checkInDate >= checkOutDate)
        {
            throw new CheckOutDateMustBeAfterCheckInDateException();
        }

        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
    }

    public DateOnly CheckInDate { get; }
    public DateOnly CheckOutDate { get; }

    public bool Overlaps(BookingDateRange other)
    {
        return CheckInDate <= other.CheckOutDate && CheckOutDate >= other.CheckInDate;
    }
}