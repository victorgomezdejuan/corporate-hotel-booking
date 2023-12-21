using CorporateHotelBooking.Repositories.Bookings;

namespace CorporateHotelBooking.Application.Employees.Commands.DeleteEmployee;

public class EmployeeBookingDeleter : IEmployeeDeletedObserver
{
    private readonly IBookingRepository _bookingRepository;

    public EmployeeBookingDeleter(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public void Notify(int employeeId)
    {
        _bookingRepository.DeleteByEmployee(employeeId);
    }
}