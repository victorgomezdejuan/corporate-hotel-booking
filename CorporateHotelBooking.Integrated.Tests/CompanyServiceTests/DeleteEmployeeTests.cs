using AutoFixture.Xunit2;
using CorporateHotelBooking.Application.Common;
using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Integrated.Tests.Helpers;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using CorporateHotelBooking.Services;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.CompanyServiceTests;

public class DeleteEmployeeTests
{
    [Theory, AutoData]
    public void DeleteEmployeeAndTheirAssociatedItems(int companyId)
    {
        // Arrange
        var booking = BookingFactory.CreateRandom();
        IEmployeeRepository employeeRepository = new InMemoryEmployeeRepository();
        IBookingRepository bookingRepository = new InMemoryBookingRepository();
        bookingRepository.Add(booking);
        IEmployeeBookingPolicyRepository employeeBookingPolicyRepository =
            new InMemoryEmployeeBookingPolicyRepository();
        employeeBookingPolicyRepository
            .Add(new EmployeeBookingPolicy(booking.EmployeeId, new List<RoomType> { booking.RoomType }));
        
        var companyService = new CompanyService(employeeRepository, bookingRepository, employeeBookingPolicyRepository);
        companyService.AddEmployee(companyId, booking.EmployeeId);

        // Act
        companyService.DeleteEmployee(booking.EmployeeId);

        // Assert
        employeeRepository.Get(booking.EmployeeId).Should().BeNull();
        bookingRepository
            .GetCount(booking.EmployeeId, booking.RoomType, booking.CheckInDate, booking.CheckOutDate)
            .Should().Be(0);
        employeeBookingPolicyRepository.Exists(booking.EmployeeId).Should().BeFalse();
    }
}