
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

    public bool BookingAllowed(RoomType standard)
    {
        return _employeeBookingPolicy.BookingAllowed(standard) || _companyBookingPolicy.BookingAllowed(standard);
    }
}