using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests.Helpers.AutoFixture;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using CorporateHotelBooking.Services;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests;

public class IsBookingAllowedTests
{
    [Theory, AutoData]
    public void BookingAllowedByCompanyPolicy(Employee employee, [CollectionSize(1)] List<RoomType> roomTypes)
    {
        // Arrange
        var employeeRepository = new InMemoryEmployeeRepository();
        employeeRepository.Add(employee);
        var companyPolicyRepository = new InMemoryCompanyBookingPolicyRepository();
        companyPolicyRepository.Add(
            new CompanyBookingPolicy(employee.CompanyId, roomTypes));
        
        var bookingPolicyService = new BookingPolicyService(
            companyPolicyRepository,
            new InMemoryEmployeeBookingPolicyRepository(),
            employeeRepository);

        // Act
        var isBookingAllowed = bookingPolicyService.IsBookingAllowed(employee.Id, roomTypes[0]);

        // Assert
        isBookingAllowed.Should().BeTrue();
    }
}