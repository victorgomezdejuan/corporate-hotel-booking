using AutoFixture.Xunit2;
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
    [Theory, AutoData]
    public void BookingAllowedByCompanyPolicy(Employee employee)
    {
        // Arrange
        var employeeRepository = new InMemoryEmployeeRepository();
        employeeRepository.Add(employee);
        var companyPolicyRepository = new InMemoryCompanyBookingPolicyRepository();
        companyPolicyRepository.Add(
            new CompanyBookingPolicy(employee.CompanyId, new List<RoomType> { RoomType.Standard }));
        
        var bookingPolicyService = new BookingPolicyService(
            companyPolicyRepository,
            new InMemoryEmployeeBookingPolicyRepository(),
            employeeRepository);

        // Act
        var isBookingAllowed = bookingPolicyService.IsBookingAllowed(employee.Id, RoomType.Standard);

        // Assert
        isBookingAllowed.Should().BeTrue();
    }
}