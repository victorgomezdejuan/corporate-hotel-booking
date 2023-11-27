
namespace CorporateHotelBooking.Domain;

public class BookingPolicy
{
    private readonly EmployeeBookingPolicy _employeeBookingPolicy;
    private readonly CompanyBookingPolicy _companyBookingPolicy;

    public BookingPolicy(EmployeeBookingPolicy employeeBookingPolicy, CompanyBookingPolicy companyBookingPolicy)
    {
        _employeeBookingPolicy = employeeBookingPolicy;
        _companyBookingPolicy = companyBookingPolicy;
    }

    public bool BookingAllowed(RoomType roomType)
    {
        if (_employeeBookingPolicy?.AllowedRoomTypes.Any() == true)
        {
            return _employeeBookingPolicy.AllowedRoomTypes.Contains(roomType);
        }

        if (_companyBookingPolicy?.AllowedRoomTypes.Any() == true)
        {
            return _companyBookingPolicy.AllowedRoomTypes.Contains(roomType);
        }

        return true;
    }
}