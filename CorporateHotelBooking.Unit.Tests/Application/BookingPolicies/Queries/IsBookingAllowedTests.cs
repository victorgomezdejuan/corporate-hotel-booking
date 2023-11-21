using CorporateHotelBooking.Application.BookingPolicies.Queries.IsBookingAllowed;
using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;
using CorporateHotelBooking.Repositories.EmployeePolicies;
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

        var employeePolicyRepositoryMock = new Mock<IEmployeePolicyRepository>();
        var companyPolicyRepositoryMock = new Mock<ICompanyPolicyRepository>();

        var query = new IsBookingAllowedQuery(1, RoomType.Standard);
        var handler = new IsBookingAllowedQueryHandler(employeeRepositoryMock.Object, companyPolicyRepositoryMock.Object, employeePolicyRepositoryMock.Object);

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

        var employeePolicyRepositoryMock = new Mock<IEmployeePolicyRepository>();
        employeePolicyRepositoryMock.Setup(x => x.Exists(1)).Returns(false);

        var companyPolicyRepositoryMock = new Mock<ICompanyPolicyRepository>();
        companyPolicyRepositoryMock.Setup(x => x.Exists(100)).Returns(true);
        companyPolicyRepositoryMock.Setup(x => x.GetCompanyPolicy(100)).Returns(new CompanyPolicy(100, new List<RoomType> { RoomType.Standard }));

        var query = new IsBookingAllowedQuery(1, RoomType.Standard);
        var handler = new IsBookingAllowedQueryHandler(employeeRepositoryMock.Object, companyPolicyRepositoryMock.Object, employeePolicyRepositoryMock.Object);

        // Act
        var result = handler.Handle(query);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void BookingAllowedByEmployeeBookingPolicy()
    {
        // Arrange
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        employeeRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        employeeRepositoryMock.Setup(x => x.GetEmployee(1)).Returns(new Employee(1, 100));

        var employeePolicyRepositoryMock = new Mock<IEmployeePolicyRepository>();
        employeePolicyRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        employeePolicyRepositoryMock.Setup(x => x.GetEmployeePolicy(1)).Returns(new EmployeePolicy(1, new List<RoomType> { RoomType.Standard }));

        var companyPolicyRepositoryMock = new Mock<ICompanyPolicyRepository>();
        companyPolicyRepositoryMock.Setup(x => x.Exists(100)).Returns(false);

        var query = new IsBookingAllowedQuery(1, RoomType.Standard);
        var handler = new IsBookingAllowedQueryHandler(employeeRepositoryMock.Object, companyPolicyRepositoryMock.Object, employeePolicyRepositoryMock.Object);

        // Act
        var result = handler.Handle(query);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void BookingAllowedByEmployeeBookingPolicyButNotByCompanyBookingPolicy()
    {
        // Arrange
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        employeeRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        employeeRepositoryMock.Setup(x => x.GetEmployee(1)).Returns(new Employee(1, 100));

        var companyPolicyRepositoryMock = new Mock<ICompanyPolicyRepository>();
        companyPolicyRepositoryMock.Setup(x => x.Exists(100)).Returns(true);
        companyPolicyRepositoryMock.Setup(x => x.GetCompanyPolicy(100)).Returns(new CompanyPolicy(100, new List<RoomType> { RoomType.JuniorSuite }));

        var employeePolicyRepositoryMock = new Mock<IEmployeePolicyRepository>();
        employeePolicyRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        employeePolicyRepositoryMock.Setup(x => x.GetEmployeePolicy(1)).Returns(new EmployeePolicy(1, new List<RoomType> { RoomType.Standard }));

        var query = new IsBookingAllowedQuery(1, RoomType.Standard);
        var handler = new IsBookingAllowedQueryHandler(employeeRepositoryMock.Object, companyPolicyRepositoryMock.Object, employeePolicyRepositoryMock.Object);

        // Act
        var result = handler.Handle(query);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void BookingAllowedByCompanyBookingPolicyButNotByEmployeeBookingPolicy()
    {
        // Arrange
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        employeeRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        employeeRepositoryMock.Setup(x => x.GetEmployee(1)).Returns(new Employee(1, 100));

        var companyPolicyRepositoryMock = new Mock<ICompanyPolicyRepository>();
        companyPolicyRepositoryMock.Setup(x => x.Exists(100)).Returns(true);
        companyPolicyRepositoryMock.Setup(x => x.GetCompanyPolicy(100)).Returns(new CompanyPolicy(100, new List<RoomType> { RoomType.Standard }));

        var employeePolicyRepositoryMock = new Mock<IEmployeePolicyRepository>();
        employeePolicyRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        employeePolicyRepositoryMock.Setup(x => x.GetEmployeePolicy(1)).Returns(new EmployeePolicy(1, new List<RoomType> { RoomType.JuniorSuite }));

        var query = new IsBookingAllowedQuery(1, RoomType.Standard);
        var handler = new IsBookingAllowedQueryHandler(employeeRepositoryMock.Object, companyPolicyRepositoryMock.Object, employeePolicyRepositoryMock.Object);

        // Act
        var result = handler.Handle(query);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void BookingNotAllowedByAnyBookingPolicy()
    {
        // Arrange
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        employeeRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        employeeRepositoryMock.Setup(x => x.GetEmployee(1)).Returns(new Employee(1, 100));

        var companyPolicyRepositoryMock = new Mock<ICompanyPolicyRepository>();
        companyPolicyRepositoryMock.Setup(x => x.Exists(100)).Returns(true);
        companyPolicyRepositoryMock.Setup(x => x.GetCompanyPolicy(100)).Returns(new CompanyPolicy(100, new List<RoomType> { RoomType.JuniorSuite }));

        var employeePolicyRepositoryMock = new Mock<IEmployeePolicyRepository>();
        employeePolicyRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        employeePolicyRepositoryMock.Setup(x => x.GetEmployeePolicy(1)).Returns(new EmployeePolicy(1, new List<RoomType> { RoomType.MasterSuite }));

        var query = new IsBookingAllowedQuery(1, RoomType.Standard);
        var handler = new IsBookingAllowedQueryHandler(employeeRepositoryMock.Object, companyPolicyRepositoryMock.Object, employeePolicyRepositoryMock.Object);

        // Act
        var result = handler.Handle(query);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void NoBookingPoliciesForAnEmployee()
    {
        // Arrange
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        employeeRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        employeeRepositoryMock.Setup(x => x.GetEmployee(1)).Returns(new Employee(1, 100));

        var companyPolicyRepositoryMock = new Mock<ICompanyPolicyRepository>();
        companyPolicyRepositoryMock.Setup(x => x.Exists(100)).Returns(false);

        var employeePolicyRepositoryMock = new Mock<IEmployeePolicyRepository>();
        employeePolicyRepositoryMock.Setup(x => x.Exists(1)).Returns(false);

        var query = new IsBookingAllowedQuery(1, RoomType.Standard);
        var handler = new IsBookingAllowedQueryHandler(employeeRepositoryMock.Object, companyPolicyRepositoryMock.Object, employeePolicyRepositoryMock.Object);

        // Act
        var result = handler.Handle(query);

        // Assert
        result.Should().BeTrue();
    }
}