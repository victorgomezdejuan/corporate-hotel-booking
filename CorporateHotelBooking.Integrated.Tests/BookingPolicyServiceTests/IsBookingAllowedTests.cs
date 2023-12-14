using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using CorporateHotelBooking.Services;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests;

public class IsBookingAllowedTests
{
    [Fact]
    public void BookingAllowedByCompanyPolicy()
    {
        // Arrange
        var employeeRepository = new InMemoryEmployeeRepository();
        employeeRepository.Add(new Employee(id: 1, companyId: 100));
        var companyPolicyRepository = new InMemoryCompanyBookingPolicyRepository();
        companyPolicyRepository.Add(new CompanyBookingPolicy(100, new List<RoomType> { RoomType.Standard }));
        var bookingPolicyService = new BookingPolicyService(
            companyPolicyRepository,
            new InMemoryEmployeeBookingPolicyRepository(),
            employeeRepository);

        // Act
        var isBookingAllowed = bookingPolicyService.IsBookingAllowed(employeeId: 1, RoomType.Standard);

        // Assert
        isBookingAllowed.Should().BeTrue();
    }
}