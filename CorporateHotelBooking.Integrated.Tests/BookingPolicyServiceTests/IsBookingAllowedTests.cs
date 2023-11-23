using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;
using CorporateHotelBooking.Repositories.EmployeePolicies;
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
        var companyPolicyRepository = new InMemoryCompanyPolicyRepository();
        companyPolicyRepository.AddCompanyPolicy(new CompanyPolicy(100, new List<RoomType> { RoomType.Standard }));
        var bookingPolicyService = new BookingPolicyService(
            companyPolicyRepository,
            new InMemoryEmployeePolicyRepository(),
            employeeRepository);

        // Act
        var isBookingAllowed = bookingPolicyService.IsBookingAllowed(employeeId: 1, RoomType.Standard);

        // Assert
        isBookingAllowed.Should().BeTrue();
    }
}