
namespace CorporateHotelBooking.Domain;

public class AggregatedBookingPolicy
{
    private readonly BookingPolicy _employeeBookingPolicy;
    private readonly BookingPolicy _companyBookingPolicy;

    public AggregatedBookingPolicy(BookingPolicy employeeBookingPolicy, BookingPolicy companyBookingPolicy)
    {
        _employeeBookingPolicy = employeeBookingPolicy;
        _companyBookingPolicy = companyBookingPolicy;
    }

    public bool BookingAllowed(RoomType roomType)
    {
        if (_employeeBookingPolicy.IsApplicable)
        {
            return _employeeBookingPolicy.BookingAllowed(roomType);
        }

        if (_companyBookingPolicy.IsApplicable)
        {
            return _companyBookingPolicy.BookingAllowed(roomType);
        }

        return true;
    }
}