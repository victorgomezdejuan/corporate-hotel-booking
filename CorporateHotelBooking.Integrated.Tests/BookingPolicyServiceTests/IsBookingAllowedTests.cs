using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests;

public class IsBookingAllowedTests
{
    [Fact]
    public void BookingAllowedByCompanyPolicy()
    {
        // Arrange
        var employeeRepository = new InMemoryEmployeeRepository();
        var companyService = new CompanyService(employeeRepository);
        companyService.AddEmployee(companyId: 100, employeeId: 1);
        var companyPolicyRepository = new InMemoryCompanyBookingPolicyRepository();
        companyPolicyRepository.AddCompanyPolicy(new CompanyBookingPolicy(100, new List<RoomType> { RoomType.Standard }));
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