using CorporateHotelBooking.Application.BookingPolicies.Queries.IsBookingAllowed;
using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.BookingPolicies.Queries;

public class IsBookingAllowedTests
{
    [Fact]
    public void NonExistingEmployee()
    {
        // Arrange
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        employeeRepositoryMock.Setup(x => x.Exists(1)).Returns(false);
        var companyPolicyRepositoryMock = new Mock<ICompanyPolicyRepository>();
        var query = new IsBookingAllowedQuery(1, RoomType.Standard);
        var handler = new IsBookingAllowedQueryHandler(employeeRepositoryMock.Object, companyPolicyRepositoryMock.Object);

        // Act
        Action act = () => handler.Handle(query);

        // Assert
        act.Should().Throw<EmployeeNotFoundException>();
    }

    [Fact]
    public void BookingAllowedByCompanyBookingPolicy()
    {
        // Arrange
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        employeeRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        employeeRepositoryMock.Setup(x => x.GetEmployee(1)).Returns(new Employee(1, 100));
        var companyPolicyRepositoryMock = new Mock<ICompanyPolicyRepository>();
        companyPolicyRepositoryMock.Setup(x => x.GetCompanyPolicy(100)).Returns(new CompanyPolicy(100, new List<RoomType> { RoomType.Standard }));
        var query = new IsBookingAllowedQuery(1, RoomType.Standard);
        var handler = new IsBookingAllowedQueryHandler(employeeRepositoryMock.Object, companyPolicyRepositoryMock.Object);

        // Act
        var result = handler.Handle(query);

        // Assert
        result.Should().BeTrue();
    }
}