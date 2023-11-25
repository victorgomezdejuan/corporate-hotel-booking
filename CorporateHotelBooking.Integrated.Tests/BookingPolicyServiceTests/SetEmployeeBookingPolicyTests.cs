using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests.Helpers;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests;

public class SetEmployeeBookingPolicyTests
{
    private readonly InMemoryEmployeeBookingPolicyRepository _employeePolicyRepository;
    private readonly BookingPolicyService _bookingPolicyService;

    public SetEmployeeBookingPolicyTests()
    {
        _employeePolicyRepository = new InMemoryEmployeeBookingPolicyRepository();
        _bookingPolicyService = new BookingPolicyService(
            // BookingPolicyService acts as a facade that handles different actions related to booking policies
            // This leads us to feed it with two additional repositories although for this use case they are not needed
            new NotImplementedCompanyBookingPolicyRepository(),
            _employeePolicyRepository,
            new NotImplementedEmployeeRepository());
    }

    [Fact]
    public void AddNewEmployeePolicy()
    {
        // Act
        _bookingPolicyService.SetEmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Assert
        var retrievedEmployeePolicy = _employeePolicyRepository.GetEmployeePolicy(1);
        retrievedEmployeePolicy.Should().Be(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));
    }

    [Fact]
    public void UpdateExistingEmployeePolicy()
    {
        // Arrange
        _bookingPolicyService.SetEmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        _bookingPolicyService.SetEmployeePolicy(1, new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // Assert
        var retrievedEmployeePolicy = _employeePolicyRepository.GetEmployeePolicy(1);
        retrievedEmployeePolicy.Should().Be(new EmployeeBookingPolicy(1, new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite }));
    }
}

public class NotImplementedCompanyBookingPolicyRepository : ICompanyBookingPolicyRepository
{
    public void AddCompanyPolicy(CompanyBookingPolicy companyPolicy)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int companyId)
    {
        throw new NotImplementedException();
    }

    public CompanyBookingPolicy GetCompanyPolicy(int companyId)
    {
        throw new NotImplementedException();
    }

    public void UpdateCompanyPolicy(CompanyBookingPolicy companyPolicy)
    {
        throw new NotImplementedException();
    }
}