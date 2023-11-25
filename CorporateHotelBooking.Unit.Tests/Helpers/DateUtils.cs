namespace CorporateHotelBooking.Unit.Tests.Helpers;

public class DateUtils
{
    public static DateOnly Today()
    {
        return DateOnly.FromDateTime(DateTime.Now);
    }
}