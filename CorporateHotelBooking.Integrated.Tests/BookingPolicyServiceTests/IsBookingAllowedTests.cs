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
        // // Arrange
        // var companyService = new CompanyService(new InMemoryEmployeeRepository());
        // companyService.AddEmployee(companyId: 100, employeeId: 1);
        // var companyPolicyRepository = new InMemoryCompanyPolicyRepository();
        // companyPolicyRepository.AddCompanyPolicy(new CompanyPolicy(100, new List<RoomType> { RoomType.Standard }));
        // var employeePolicyRepository = new InMemoryEmployeePolicyRepository();
        // var bookingPolicyService = new BookingPolicyService(companyPolicyRepository, employeePolicyRepository);

        // // Act
        // var isBookingAllowed = bookingPolicyService.IsBookingAllowed(employeeId: 1, RoomType.Standard);

        // // Assert
        // isBookingAllowed.Should().BeTrue();
    }
}