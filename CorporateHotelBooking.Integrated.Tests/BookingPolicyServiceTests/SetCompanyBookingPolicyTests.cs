using AutoFixture.Xunit2;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Integrated.Tests.Helpers;
using CorporateHotelBooking.Integrated.Tests.Helpers.AutoFixture;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Services;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests;

public class SetCompanyBookingPolicyTests
{
    private readonly InMemoryCompanyBookingPolicyRepository _companyPolicyRepository;
    private readonly BookingPolicyService _bookingPolicyService;

    public SetCompanyBookingPolicyTests()
    {
        _companyPolicyRepository = new InMemoryCompanyBookingPolicyRepository();
        _bookingPolicyService = new BookingPolicyService(
            new NotImplementedEmployeeRepository(),
            _companyPolicyRepository,
            // BookingPolicyService acts as a facade that handles different actions related to booking policies
            // This leads us to feed it with two additional repositories although for this use case they are not needed
            new NotImplementedEmployeeBookingPolicyRepository()); 
    }

    [Theory, AutoData]
    public void AddNewCompanyPolicy(int companyId, [CollectionSize(2)] List<RoomType> roomTypes)
    {
        // Act
        var result = _bookingPolicyService.SetCompanyPolicy(companyId, roomTypes);

        // Assert
        result.IsFailure.Should().BeFalse();
        var retrievedCompanyPolicy = _companyPolicyRepository.Get(companyId);
        retrievedCompanyPolicy.Should().Be(new CompanyBookingPolicy(companyId, roomTypes));
    }

    [Theory, AutoData]
    public void UpdateExistingCompanyPolicy(int companyId)
    {
        // Arrange
        _bookingPolicyService.SetCompanyPolicy(
            companyId,
            new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        var result = _bookingPolicyService.SetCompanyPolicy(
            companyId,
            new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // Assert
        result.IsFailure.Should().BeFalse();
        var retrievedCompanyPolicy = _companyPolicyRepository.Get(companyId);
        retrievedCompanyPolicy.Should().Be(new CompanyBookingPolicy(
            companyId,
            new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite }));
    }
}

public class NotImplementedEmployeeBookingPolicyRepository : IEmployeeBookingPolicyRepository
{
    public void Add(EmployeeBookingPolicy employeePolicy)
    {
        throw new NotImplementedException();
    }

    public void Delete(int employeeId)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int employeeId)
    {
        throw new NotImplementedException();
    }

    public EmployeeBookingPolicy Get(int employeeId)
    {
        throw new NotImplementedException();
    }

    public void Update(EmployeeBookingPolicy employeePolicy)
    {
        throw new NotImplementedException();
    }
}
