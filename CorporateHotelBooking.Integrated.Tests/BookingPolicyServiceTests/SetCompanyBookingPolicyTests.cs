using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests.Helpers;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
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
            _companyPolicyRepository,
            // BookingPolicyService acts as a facade that handles different actions related to booking policies
            // This leads us to feed it with two additional repositories although for this use case they are not needed
            new NotImplementedEmployeeBookingPolicyRepository(),
            new NotImplementedEmployeeRepository()); 
    }

    [Fact]
    public void AddNewCompanyPolicy()
    {
        // Act
        _bookingPolicyService.SetCompanyPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Assert
        var retrievedCompanyPolicy = _companyPolicyRepository.Get(100);
        retrievedCompanyPolicy.Should().Be(new CompanyBookingPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite }));
    }

    [Fact]
    public void UpdateExistingCompanyPolicy()
    {
        // Arrange
        _bookingPolicyService.SetCompanyPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        _bookingPolicyService.SetCompanyPolicy(100, new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // Assert
        var retrievedCompanyPolicy = _companyPolicyRepository.Get(100);
        retrievedCompanyPolicy.Should().Be(new CompanyBookingPolicy(100, new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite }));
    }
}

public class NotImplementedEmployeeBookingPolicyRepository : IEmployeeBookingPolicyRepository
{
    public void AddEmployeePolicy(EmployeeBookingPolicy employeePolicy)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int employeeId)
    {
        throw new NotImplementedException();
    }

    public EmployeeBookingPolicy GetEmployeePolicy(int employeeId)
    {
        throw new NotImplementedException();
    }

    public void UpdateEmployeePolicy(EmployeeBookingPolicy employeePolicy)
    {
        throw new NotImplementedException();
    }
}
