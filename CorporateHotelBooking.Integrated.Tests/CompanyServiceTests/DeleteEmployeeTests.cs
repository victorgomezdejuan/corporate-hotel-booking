using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using CorporateHotelBooking.Services;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.CompanyServiceTests;

public class DeleteEmployeeTests
{
    [Fact]
    public void DeleteEmployeeAndTheirAssociatedItems()
    {
        // Arrange
        IEmployeeRepository employeeRepository = new InMemoryEmployeeRepository();
        IBookingRepository bookingRepository = new InMemoryBookingRepository();
        bookingRepository.Add(new Booking(employeeId: 1, hotelId: 10, RoomType.Standard, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2)));
        IEmployeeBookingPolicyRepository employeeBookingPolicyRepository = new InMemoryEmployeeBookingPolicyRepository();
        employeeBookingPolicyRepository.Add(new EmployeeBookingPolicy(employeeId: 1, new List<RoomType> { RoomType.Standard }));
        
        var companyService = new CompanyService(employeeRepository, bookingRepository, employeeBookingPolicyRepository);
        companyService.AddEmployee(companyId: 100, employeeId: 1);

        // Act
        companyService.DeleteEmployee(1);

        // Assert
        Action action = () => employeeRepository.Get(1);
        action.Should().Throw<EmployeeNotFoundException>();
        bookingRepository.GetCount(1, RoomType.Standard, new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2)).Should().Be(0);
        employeeBookingPolicyRepository.Exists(1).Should().BeFalse();
    }
}