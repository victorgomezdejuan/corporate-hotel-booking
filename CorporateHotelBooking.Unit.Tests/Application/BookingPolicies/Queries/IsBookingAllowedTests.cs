using AutoFixture.Xunit2;
using CorporateHotelBooking.Application.BookingPolicies.Queries.IsBookingAllowed;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;
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


    [Theory, AutoData]
    public void NonExistingEmployee(int employeeId, RoomType roomType)
    {
        // Arrange
        _employeeRepositoryMock.Setup(x => x.Exists(employeeId)).Returns(false);

        var query = new IsBookingAllowedQuery(employeeId, roomType);

        // Act
        var result = _handler.Handle(query);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Employee not found.");
    }

    [Theory, AutoData]
    public void BookingAllowedByCompanyBookingPolicy(
        int employeeId,
        int companyId,
        [CollectionSize(1)] List<RoomType> roomTypes)
    {
        // Arrange
        SetEmployeeRepositoryWithEmployeeAndCompany(employeeId, companyId);

        _employeePolicyRepositoryMock.Setup(x => x.Exists(companyId)).Returns(false);

        _companyPolicyRepositoryMock.Setup(x => x.Exists(companyId)).Returns(true);
        _companyPolicyRepositoryMock.Setup(x => x.Get(companyId))
            .Returns(new CompanyBookingPolicy(companyId, roomTypes));

        var query = new IsBookingAllowedQuery(employeeId, roomTypes[0]);

        // Act
        var result = _handler.Handle(query);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.Value.Should().BeTrue();
    }

    [Theory, AutoData]
    public void BookingAllowedByEmployeeBookingPolicy(
        int employeeId,
        int companyId,
        [CollectionSize(1)] List<RoomType> roomTypes)
    {
        // Arrange
        SetEmployeeRepositoryWithEmployeeAndCompany(employeeId, companyId);

        _employeePolicyRepositoryMock.Setup(x => x.Exists(employeeId)).Returns(true);
        _employeePolicyRepositoryMock.Setup(x => x.Get(employeeId))
            .Returns(new EmployeeBookingPolicy(1, roomTypes));

        _companyPolicyRepositoryMock.Setup(x => x.Exists(companyId)).Returns(false);

        var query = new IsBookingAllowedQuery(employeeId, roomTypes[0]);

        // Act
        var result = _handler.Handle(query);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.Value.Should().BeTrue();
    }

    [Theory, AutoData]
    public void BookingAllowedByEmployeeBookingPolicyButNotByCompanyBookingPolicy(int employeeId, int companyId)
    {
        // Arrange
        SetEmployeeRepositoryWithEmployeeAndCompany(employeeId, companyId);

        _companyPolicyRepositoryMock.Setup(x => x.Exists(companyId)).Returns(true);
        _companyPolicyRepositoryMock.Setup(x => x.Get(companyId))
            .Returns(new CompanyBookingPolicy(companyId, new List<RoomType> { RoomType.JuniorSuite }));

        _employeePolicyRepositoryMock.Setup(x => x.Exists(employeeId)).Returns(true);
        _employeePolicyRepositoryMock.Setup(x => x.Get(employeeId))
            .Returns(new EmployeeBookingPolicy(employeeId, new List<RoomType> { RoomType.Standard }));

        var query = new IsBookingAllowedQuery(employeeId, RoomType.Standard);

        // Act
        var result = _handler.Handle(query);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.Value.Should().BeTrue();
    }

    [Theory, AutoData]
    public void BookingAllowedByCompanyBookingPolicyButNotByEmployeeBookingPolicy(int employeeId, int companyId)
    {
        // Arrange
        SetEmployeeRepositoryWithEmployeeAndCompany(employeeId, companyId);

        _companyPolicyRepositoryMock.Setup(x => x.Exists(companyId)).Returns(true);
        _companyPolicyRepositoryMock.Setup(x => x.Get(companyId))
            .Returns(new CompanyBookingPolicy(companyId, new List<RoomType> { RoomType.Standard }));

        _employeePolicyRepositoryMock.Setup(x => x.Exists(employeeId)).Returns(true);
        _employeePolicyRepositoryMock.Setup(x => x.Get(employeeId))
            .Returns(new EmployeeBookingPolicy(employeeId, new List<RoomType> { RoomType.JuniorSuite }));

        var query = new IsBookingAllowedQuery(employeeId, RoomType.Standard);

        // Act
        var result = _handler.Handle(query);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.Value.Should().BeFalse();
    }

    [Theory, AutoData]
    public void BookingNotAllowedByAnyBookingPolicy(int employeeId, int companyId)
    {
        // Arrange
        SetEmployeeRepositoryWithEmployeeAndCompany(employeeId, companyId);

        _companyPolicyRepositoryMock.Setup(x => x.Exists(companyId)).Returns(true);
        _companyPolicyRepositoryMock.Setup(x => x.Get(companyId))
            .Returns(new CompanyBookingPolicy(companyId, new List<RoomType> { RoomType.JuniorSuite }));

        _employeePolicyRepositoryMock.Setup(x => x.Exists(employeeId)).Returns(true);
        _employeePolicyRepositoryMock.Setup(x => x.Get(employeeId))
            .Returns(new EmployeeBookingPolicy(employeeId, new List<RoomType> { RoomType.MasterSuite }));

        var query = new IsBookingAllowedQuery(employeeId, RoomType.Standard);

        // Act
        var result = _handler.Handle(query);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.Value.Should().BeFalse();
    }

    [Theory, AutoData]
    public void NoBookingPoliciesForAnEmployee(int employeeId, int companyId)
    {
        // Arrange
        SetEmployeeRepositoryWithEmployeeAndCompany(employeeId, companyId);

        _companyPolicyRepositoryMock.Setup(x => x.Exists(companyId)).Returns(false);
        _employeePolicyRepositoryMock.Setup(x => x.Exists(employeeId)).Returns(false);

        var query = new IsBookingAllowedQuery(employeeId, RoomType.Standard);

        // Act
        var result = _handler.Handle(query);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.Value.Should().BeTrue();
    }

    private void SetEmployeeRepositoryWithEmployeeAndCompany(int employeeId, int companyId)
    {
        _employeeRepositoryMock.Setup(x => x.Exists(employeeId)).Returns(true);
        _employeeRepositoryMock.Setup(x => x.Get(employeeId)).Returns(new Employee(employeeId, companyId));
    }
}