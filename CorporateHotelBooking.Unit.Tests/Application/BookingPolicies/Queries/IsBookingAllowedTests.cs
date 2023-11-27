using CorporateHotelBooking.Application.BookingPolicies.Queries.IsBookingAllowed;
using CorporateHotelBooking.Application.Common.Exceptions;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using FluentAssertions;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.BookingPolicies.Queries;

public class IsBookingAllowedTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly Mock<ICompanyBookingPolicyRepository> _companyPolicyRepositoryMock;
    private readonly Mock<IEmployeeBookingPolicyRepository> _employeePolicyRepositoryMock;
    private readonly IsBookingAllowedQueryHandler _handler;

    public IsBookingAllowedTests()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        _companyPolicyRepositoryMock = new Mock<ICompanyBookingPolicyRepository>();
        _employeePolicyRepositoryMock = new Mock<IEmployeeBookingPolicyRepository>();
        _handler = new IsBookingAllowedQueryHandler(
            _employeeRepositoryMock.Object,
            _companyPolicyRepositoryMock.Object,
            _employeePolicyRepositoryMock.Object);
    }


    [Fact]
    public void NonExistingEmployee()
    {
        // Arrange
        _employeeRepositoryMock.Setup(x => x.Exists(1)).Returns(false);

        var query = new IsBookingAllowedQuery(1, RoomType.Standard);

        // Act
        Action act = () => _handler.Handle(query);

        // Assert
        act.Should().Throw<EmployeeNotFoundException>();
    }

    [Fact]
    public void BookingAllowedByCompanyBookingPolicy()
    {
        // Arrange
        SetEmployeeRepositoryWithEmployeeAndCompany(1, 100);

        _employeePolicyRepositoryMock.Setup(x => x.Exists(1)).Returns(false);

        _companyPolicyRepositoryMock.Setup(x => x.Exists(100)).Returns(true);
        _companyPolicyRepositoryMock.Setup(x => x.GetCompanyPolicy(100)).Returns(new CompanyBookingPolicy(100, new List<RoomType> { RoomType.Standard }));

        var query = new IsBookingAllowedQuery(1, RoomType.Standard);

        // Act
        var result = _handler.Handle(query);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void BookingAllowedByEmployeeBookingPolicy()
    {
        // Arrange
        SetEmployeeRepositoryWithEmployeeAndCompany(1, 100);

        _employeePolicyRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        _employeePolicyRepositoryMock.Setup(x => x.GetEmployeePolicy(1)).Returns(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard }));

        _companyPolicyRepositoryMock.Setup(x => x.Exists(100)).Returns(false);

        var query = new IsBookingAllowedQuery(1, RoomType.Standard);

        // Act
        var result = _handler.Handle(query);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void BookingAllowedByEmployeeBookingPolicyButNotByCompanyBookingPolicy()
    {
        // Arrange
        SetEmployeeRepositoryWithEmployeeAndCompany(1, 100);

        _companyPolicyRepositoryMock.Setup(x => x.Exists(100)).Returns(true);
        _companyPolicyRepositoryMock.Setup(x => x.GetCompanyPolicy(100)).Returns(new CompanyBookingPolicy(100, new List<RoomType> { RoomType.JuniorSuite }));

        _employeePolicyRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        _employeePolicyRepositoryMock.Setup(x => x.GetEmployeePolicy(1)).Returns(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard }));

        var query = new IsBookingAllowedQuery(1, RoomType.Standard);

        // Act
        var result = _handler.Handle(query);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void BookingAllowedByCompanyBookingPolicyButNotByEmployeeBookingPolicy()
    {
        // Arrange
        SetEmployeeRepositoryWithEmployeeAndCompany(1, 100);

        _companyPolicyRepositoryMock.Setup(x => x.Exists(100)).Returns(true);
        _companyPolicyRepositoryMock.Setup(x => x.GetCompanyPolicy(100)).Returns(new CompanyBookingPolicy(100, new List<RoomType> { RoomType.Standard }));

        _employeePolicyRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        _employeePolicyRepositoryMock.Setup(x => x.GetEmployeePolicy(1)).Returns(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.JuniorSuite }));

        var query = new IsBookingAllowedQuery(1, RoomType.Standard);

        // Act
        var result = _handler.Handle(query);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void BookingNotAllowedByAnyBookingPolicy()
    {
        // Arrange
        SetEmployeeRepositoryWithEmployeeAndCompany(1, 100);

        _companyPolicyRepositoryMock.Setup(x => x.Exists(100)).Returns(true);
        _companyPolicyRepositoryMock.Setup(x => x.GetCompanyPolicy(100)).Returns(new CompanyBookingPolicy(100, new List<RoomType> { RoomType.JuniorSuite }));

        _employeePolicyRepositoryMock.Setup(x => x.Exists(1)).Returns(true);
        _employeePolicyRepositoryMock.Setup(x => x.GetEmployeePolicy(1)).Returns(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.MasterSuite }));

        var query = new IsBookingAllowedQuery(1, RoomType.Standard);

        // Act
        var result = _handler.Handle(query);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void NoBookingPoliciesForAnEmployee()
    {
        // Arrange
        SetEmployeeRepositoryWithEmployeeAndCompany(1, 100);

        _companyPolicyRepositoryMock.Setup(x => x.Exists(100)).Returns(false);
        _employeePolicyRepositoryMock.Setup(x => x.Exists(1)).Returns(false);

        var query = new IsBookingAllowedQuery(1, RoomType.Standard);

        // Act
        var result = _handler.Handle(query);

        // Assert
        result.Should().BeTrue();
    }

    private void SetEmployeeRepositoryWithEmployeeAndCompany(int employeeId, int companyId)
    {
        _employeeRepositoryMock.Setup(x => x.Exists(employeeId)).Returns(true);
        _employeeRepositoryMock.Setup(x => x.GetEmployee(employeeId)).Returns(new Employee(employeeId, companyId));
    }
}